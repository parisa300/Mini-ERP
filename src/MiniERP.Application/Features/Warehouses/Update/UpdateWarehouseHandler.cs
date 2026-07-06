using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Warehouses.Update;

public class UpdateWarehouseHandler
{
    private readonly IApplicationDbContext _context;

    public UpdateWarehouseHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(
        UpdateWarehouseCommand command,
        CancellationToken cancellationToken)
    {
        var warehouse = await _context.Warehouses
            .FirstOrDefaultAsync(
                x => x.Id == command.Id,
                cancellationToken);

        if (warehouse is null)
            throw new Exception("Warehouse not found.");

        var duplicateCode = await _context.Warehouses
            .AnyAsync(x =>
                x.Code == command.Code &&
                x.Id != command.Id,
                cancellationToken);

        if (duplicateCode)
            throw new Exception("Warehouse code already exists.");

        warehouse.Update(
            command.Name,
            command.Code,
            command.Address,
            command.Description);

        await _context.SaveChangesAsync(cancellationToken);
    }
}