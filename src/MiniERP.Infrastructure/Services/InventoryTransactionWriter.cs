using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;
using MiniERP.Domain.Enums;

namespace MiniERP.Infrastructure.Services;

public class InventoryTransactionWriter : IInventoryTransactionWriter
{
    private readonly IApplicationDbContext _context;

    public InventoryTransactionWriter(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task WriteAsync(
        Inventory inventory,
        int quantity,
        InventoryTransactionType type,
        string? referenceNumber,
        string? description,
        CancellationToken cancellationToken)
    {
        var transaction = new InventoryTransaction(
            inventory.Id,
            quantity,
            type,
            referenceNumber,
            description);

        await _context.InventoryTransactions.AddAsync(
            transaction,
            cancellationToken);
    }
}