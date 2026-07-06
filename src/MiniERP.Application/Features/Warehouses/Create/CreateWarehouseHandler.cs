using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.Warehouses.Create;

public class CreateWarehouseHandler
{
    private readonly IApplicationDbContext _context;

    public CreateWarehouseHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateWarehouseCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Warehouses
            .AnyAsync(x => x.Code == command.Code, cancellationToken);

        if (exists)
            throw new Exception("Warehouse code already exists.");

        var warehouse = new Warehouse(
            command.Name,
            command.Code,
            command.Address,
            command.Description);

        _context.Warehouses.Add(warehouse);

        await _context.SaveChangesAsync(cancellationToken);

        return warehouse.Id;
    }
}