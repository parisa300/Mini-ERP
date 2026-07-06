using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Shared.Models;

namespace MiniERP.Application.Features.Customers.GetAll;

public class GetAllCustomersHandler
{
    private readonly IApplicationDbContext _context;

    public GetAllCustomersHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<CustomerDto>> Handle(
        GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Customer> query = _context.Customers
            .AsNoTracking()
            .Where(x => x.IsActive);

        // Search
  if (!string.IsNullOrWhiteSpace(request.Search))
{
    query = query.Where(x =>
        x.FullName.Contains(request.Search) ||
        x.PhoneNumber.Contains(request.Search));
}

        // Sort
        query = request.SortBy switch
        {
            "name_desc" => query.OrderByDescending(x => x.FullName),

            "created" => query.OrderBy(x => x.CreatedAt),

            "created_desc" => query.OrderByDescending(x => x.CreatedAt),

            _ => query.OrderBy(x => x.FullName)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new CustomerDto(
                x.Id,
                x.FullName,
                x.PhoneNumber,
                x.Email,
                x.Address))
            .ToListAsync(cancellationToken);

        return new PagedResult<CustomerDto>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}