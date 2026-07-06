using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Common.Extensions;
using MiniERP.Shared.Models;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.Products.GetAll;

public class GetAllProductsHandler
{
    private readonly IApplicationDbContext _context;

    public GetAllProductsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<ProductDto>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
       IQueryable<Product> query = _context.Products
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Include(x => x.Category);

        // Search
        if (!string.IsNullOrWhiteSpace(request.Filter.Search))
        {
            query = query.Where(x =>
                x.Name.Contains(request.Filter.Search));
        }

        // Category Filter
        if (request.Filter.CategoryId.HasValue)
        {
            query = query.Where(x =>
                x.CategoryId == request.Filter.CategoryId.Value);
        }

        // Min Price
        if (request.Filter.MinPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price >= request.Filter.MinPrice.Value);
        }

        // Max Price
        if (request.Filter.MaxPrice.HasValue)
        {
            query = query.Where(x =>
                x.Price <= request.Filter.MaxPrice.Value);
        }

        // Sorting
        query = request.Filter.SortBy?.ToLower() switch
        {
            "price" => query.OrderBy(x => x.Price),

            "price_desc" => query.OrderByDescending(x => x.Price),

            "stock" => query.OrderBy(x => x.Stock),

            "stock_desc" => query.OrderByDescending(x => x.Stock),

            _ => query.OrderBy(x => x.Name)
        };

        var result = await query
            .Select(x => new ProductDto(
                x.Id,
                x.Name,
                x.Description,
                x.Price,
                x.Stock,
                x.Category.Name))
            .ToPagedResultAsync(
                request.Pagination,
                cancellationToken);

        return result;
    }
}