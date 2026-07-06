using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Products.GetById;

public class GetProductByIdHandler
{
    private readonly IApplicationDbContext _context;

    public GetProductByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDetailsDto?> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.Id == query.Id && x.IsActive)
            .Select(x => new ProductDetailsDto(
                x.Id,
                x.Name,
                x.Description,
                x.Price,
                x.Stock,
                x.CategoryId,
                x.Category.Name))
            .FirstOrDefaultAsync(cancellationToken);
    }
}