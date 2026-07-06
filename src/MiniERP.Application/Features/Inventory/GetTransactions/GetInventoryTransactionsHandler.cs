using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Common.Exceptions;

namespace MiniERP.Application.Features.Inventory.GetTransactions;

public class GetInventoryTransactionsHandler
{
    private readonly IApplicationDbContext _context;

    public GetInventoryTransactionsHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<InventoryTransactionDto>> Handle(
        GetInventoryTransactionsQuery query,
        CancellationToken cancellationToken)
    {
        var inventoryExists = await _context.Inventories
            .AnyAsync(x => x.Id == query.InventoryId, cancellationToken);

        if (!inventoryExists)
            throw new NotFoundException("Inventory not found.");

        return await _context.InventoryTransactions
            .Where(x => x.InventoryId == query.InventoryId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new InventoryTransactionDto
            {
                TransactionType = x.TransactionType,
                Quantity = x.Quantity,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}