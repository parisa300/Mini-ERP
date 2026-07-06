using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;
using MiniERP.Domain.Enums;

namespace MiniERP.Application.Features.Inventory.Transfer;

public class TransferInventoryHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IInventoryTransactionWriter _transactionWriter;

    public TransferInventoryHandler(
        IApplicationDbContext context,
        IInventoryTransactionWriter transactionWriter)
    {
        _context = context;
        _transactionWriter = transactionWriter;
    }

public async Task Handle(
    TransferInventoryCommand command,
    CancellationToken cancellationToken)
{
    await using var transaction =
        await _context.Database.BeginTransactionAsync(cancellationToken);

    try
    {
        var sourceInventory = await _context.Inventories
            .FirstOrDefaultAsync(x =>
                x.ProductId == command.ProductId &&
                x.WarehouseId == command.FromWarehouseId,
                cancellationToken);

        if (sourceInventory is null)
            throw new NotFoundException("Source inventory not found.");

        var destinationInventory = await _context.Inventories
            .FirstOrDefaultAsync(x =>
                x.ProductId == command.ProductId &&
                x.WarehouseId == command.ToWarehouseId,
                cancellationToken);

        if (destinationInventory is null)
        {
            destinationInventory = new MiniERP.Domain.Entities.Inventory(
                command.ProductId,
                command.ToWarehouseId,
                0);

            _context.Inventories.Add(destinationInventory);
        }

        sourceInventory.Decrease(command.Quantity);

        destinationInventory.Increase(command.Quantity);

        await _transactionWriter.WriteAsync(
            sourceInventory,
            command.Quantity,
            InventoryTransactionType.TransferOut,
            null,
            "Transfer Out",
            cancellationToken);

        await _transactionWriter.WriteAsync(
            destinationInventory,
            command.Quantity,
            InventoryTransactionType.TransferIn,
            null,
            "Transfer In",
            cancellationToken);

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