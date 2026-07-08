using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Enums;
using InventoryEntity = MiniERP.Domain.Entities.Inventory;

namespace MiniERP.Application.Features.PurchaseOrders.Receive;

public class ReceivePurchaseOrderHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IInventoryTransactionWriter _transactionWriter;
    private readonly IApplicationLogger<ReceivePurchaseOrderHandler> _logger;

    public ReceivePurchaseOrderHandler(
        IApplicationDbContext context,
        IInventoryTransactionWriter transactionWriter,
        IApplicationLogger<ReceivePurchaseOrderHandler> logger)
    {
        _context = context;
        _transactionWriter = transactionWriter;
        _logger = logger;
    }

    public async Task Handle(
        ReceivePurchaseOrderCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Receiving purchase order. Order:{PurchaseOrderId}",
            command.PurchaseOrderId);

        await using var dbTransaction =
            await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var order = await _context.PurchaseOrders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.Id == command.PurchaseOrderId,
                    cancellationToken);

            if (order is null)
            {
                _logger.LogWarning(
                    "Purchase order not found. Order:{PurchaseOrderId}",
                    command.PurchaseOrderId);

                throw new NotFoundException("Purchase order not found.");
            }

            order.Receive();

            var referenceNumber = order.Id.ToString();

            foreach (var item in order.Items)
            {
                var inventory = await _context.Inventories
                    .FirstOrDefaultAsync(
                        x => x.ProductId == item.ProductId &&
                             x.WarehouseId == order.WarehouseId,
                        cancellationToken);

                if (inventory is null)
                {
                    inventory = new InventoryEntity(
                        item.ProductId,
                        order.WarehouseId,
                        0);

                    _context.Inventories.Add(inventory);

                    _logger.LogInformation(
                        "Inventory created. Product:{ProductId}, Warehouse:{WarehouseId}",
                        item.ProductId,
                        order.WarehouseId);
                }

                inventory.Increase(item.Quantity);

                await _transactionWriter.WriteAsync(
                    inventory,
                    item.Quantity,
                    InventoryTransactionType.Receive,
                    referenceNumber,
                    $"Purchase Order {order.Id}",
                    cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            await dbTransaction.CommitAsync(cancellationToken);

            _logger.LogInformation(
                "Purchase order received successfully. Order:{PurchaseOrderId}",
                order.Id);
        }
        catch (Exception ex)
        {
            await dbTransaction.RollbackAsync(cancellationToken);

            _logger.LogError(
                ex,
                "Receiving purchase order failed. Order:{PurchaseOrderId}",
                command.PurchaseOrderId);

            throw;
        }
    }
}