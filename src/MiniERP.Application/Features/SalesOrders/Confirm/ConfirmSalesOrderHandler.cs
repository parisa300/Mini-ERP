using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Enums;

namespace MiniERP.Application.Features.SalesOrders.Confirm;

public class ConfirmSalesOrderHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IInventoryTransactionWriter _transactionWriter;

    public ConfirmSalesOrderHandler(
        IApplicationDbContext context,
        IInventoryTransactionWriter transactionWriter)
    {
        _context = context;
        _transactionWriter = transactionWriter;
    }

    public async Task Handle(
        ConfirmSalesOrderCommand command,
        CancellationToken cancellationToken)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var order = await _context.SalesOrders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.Id == command.SalesOrderId,
                    cancellationToken);

            if (order is null)
                throw new NotFoundException("Sales order not found.");

            foreach (var item in order.Items)
            {
                var inventory = await _context.Inventories
                    .FirstOrDefaultAsync(x =>
                        x.ProductId == item.ProductId &&
                        x.WarehouseId == order.WarehouseId,
                        cancellationToken);

                if (inventory is null)
                    throw new NotFoundException(
                        $"Inventory not found for Product {item.ProductId}");

                inventory.Decrease(item.Quantity);

                await _transactionWriter.WriteAsync(
                    inventory,
                    item.Quantity,
                    InventoryTransactionType.Issue,
                    order.Id.ToString(),
                    $"Sales Order {order.Id}",
                    cancellationToken);
            }

            order.Confirm();

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}