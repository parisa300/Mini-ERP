using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Shared.Models;

namespace MiniERP.Application.Features.Warehouses.GetAll;

public class GetAllWarehousesHandler
{
    private readonly IApplicationDbContext _context;

    public GetAllWarehousesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<WarehouseDto>> Handle(
        GetAllWarehousesQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Warehouse> query = _context.Warehouses
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Search))
{
    query = query.Where(x =>
        x.Name.Contains(request.Search) ||
        x.Code.Contains(request.Search));
}

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new WarehouseDto(
                x.Id,
                x.Name,
                x.Code,
                x.Address,
                x.Description))
            .ToListAsync(cancellationToken);

        return new PagedResult<WarehouseDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}