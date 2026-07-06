using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Features.Warehouses.GetAll;

namespace MiniERP.Application.Features.Warehouses.GetById;

public class GetWarehouseByIdHandler
{
    private readonly IApplicationDbContext _context;

    public GetWarehouseByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WarehouseDto> Handle(
        GetWarehouseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var warehouse = await _context.Warehouses
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (warehouse is null)
            throw new Exception("Warehouse not found.");

        return new WarehouseDto(
            warehouse.Id,
            warehouse.Name,
            warehouse.Code,
            warehouse.Address,
            warehouse.Description);
    }
}