using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Exceptions;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Enums;

namespace MiniERP.Application.Features.Inventory.Issue;

public class IssueInventoryHandler
{
    private readonly IApplicationDbContext _context;
    private readonly IInventoryTransactionWriter _transactionWriter;

    public IssueInventoryHandler(
        IApplicationDbContext context,
        IInventoryTransactionWriter transactionWriter)
    {
        _context = context;
        _transactionWriter = transactionWriter;
    }

    public async Task Handle(
        IssueInventoryCommand command,
        CancellationToken cancellationToken)
    {
        var inventory = await _context.Inventories
            .FirstOrDefaultAsync(
                x => x.ProductId == command.ProductId &&
                     x.WarehouseId == command.WarehouseId,
                cancellationToken);

        if (inventory is null)
            throw new NotFoundException("Inventory not found.");

        inventory.Decrease(command.Quantity);

        await _transactionWriter.WriteAsync(
            inventory,
            command.Quantity,
            InventoryTransactionType.Issue,
            null,
            "Issue Inventory",
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }
}