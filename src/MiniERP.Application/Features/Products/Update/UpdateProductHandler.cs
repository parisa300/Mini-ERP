using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Products.Update;

public class UpdateProductHandler
{
    private readonly IApplicationDbContext _context;

    public UpdateProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.IsActive, cancellationToken);

        if (product is null)
            return false;

        var categoryExists = await _context.Categories
            .AnyAsync(x => x.Id == command.CategoryId && x.IsActive, cancellationToken);

        if (!categoryExists)
            throw new Exception("Category not found.");

        product.Update(
            command.Name,
            command.Description,
            command.Price,
            command.Stock,
            command.CategoryId);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}