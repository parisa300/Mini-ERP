using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Shared.Models;

namespace MiniERP.Application.Features.Suppliers.GetAll;

public class GetAllSuppliersHandler
{
    private readonly IApplicationDbContext _context;

    public GetAllSuppliersHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<SupplierDto>> Handle(
        GetAllSuppliersQuery query,
        CancellationToken cancellationToken)
    {
        var suppliers = _context.Suppliers.AsNoTracking();

        var totalCount = await suppliers.CountAsync(cancellationToken);

        var items = await suppliers
            .OrderBy(x => x.Name)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(x => new SupplierDto(
                x.Id,
                x.Name,
                x.PhoneNumber,
                x.Email,
                x.Address))
            .ToListAsync(cancellationToken);

        return new PagedResult<SupplierDto>
        {
            Items = items,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount
        };
    }
}