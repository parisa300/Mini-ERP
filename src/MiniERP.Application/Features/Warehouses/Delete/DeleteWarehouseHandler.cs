using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Warehouses.Delete;

public class DeleteWarehouseHandler
{
    private readonly IApplicationDbContext _context;

    public DeleteWarehouseHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        DeleteWarehouseCommand command,
        CancellationToken cancellationToken)
    {
        var warehouse = await _context.Warehouses
            .FirstOrDefaultAsync(
                x => x.Id == command.Id,
                cancellationToken);

        if (warehouse is null)
            throw new Exception("Warehouse not found.");

        _context.Warehouses.Remove(warehouse);

        await _context.SaveChangesAsync(cancellationToken);
    }
}