using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Shared.Models;

namespace MiniERP.Application.Features.Inventory.GetInventories;

public class GetInventoriesHandler
{
    private readonly IApplicationDbContext _context;

    public GetInventoriesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<InventoryDto>> Handle(
        GetInventoriesQuery query,
        CancellationToken cancellationToken)
    {
        var inventories = _context.Inventories
            .AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.Warehouse)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            inventories = inventories.Where(x =>
                x.Product.Name.Contains(query.Search));
        }

        if (query.WarehouseId.HasValue)
        {
            inventories = inventories.Where(x =>
                x.WarehouseId == query.WarehouseId);
        }

        var totalCount = await inventories.CountAsync(cancellationToken);

        var items = await inventories
            .OrderBy(x => x.Product.Name)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(x => new InventoryDto
            {
                InventoryId = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                WarehouseId = x.WarehouseId,
                WarehouseName = x.Warehouse.Name,
                Quantity = x.Quantity
            })
            .ToListAsync(cancellationToken);

   return new PagedResult<InventoryDto>
{
    Items = items,
    TotalCount = totalCount,
    Page = query.Page,
    PageSize = query.PageSize
};
    }
}