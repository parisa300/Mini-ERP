using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Enums;

namespace MiniERP.Application.Features.Inventory.Receive;

public class ReceiveInventoryHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IInventoryTransactionWriter _transactionWriter;

    public ReceiveInventoryHandler(IApplicationDbContext context,IInventoryTransactionWriter transactionWriter)
    {
        _context = context;
        _transactionWriter = transactionWriter;
    }

public async Task Handle(
    ReceiveInventoryCommand command,
    CancellationToken cancellationToken)
{
    var inventory = await _context.Inventories
        .FirstOrDefaultAsync(
            x => x.ProductId == command.ProductId &&
                 x.WarehouseId == command.WarehouseId,
            cancellationToken);

    if (inventory is null)
       throw new NotFoundException("Inventory not found.");

    inventory.Increase(command.Quantity);

    await _transactionWriter.WriteAsync(
        inventory,
        command.Quantity,
        InventoryTransactionType.Receive,
        null,
        "Receive Inventory",
        cancellationToken);

    await _context.SaveChangesAsync(cancellationToken);
}
}