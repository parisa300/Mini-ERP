using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Domain.Entities;
using MiniERP.Shared.Common;
namespace MiniERP.Application.Features.Products.Create;

public class CreateProductHandler
{
    private readonly IApplicationDbContext _context;

    public CreateProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var categoryExists = await _context.Categories
            .AnyAsync(x => x.Id == command.CategoryId && x.IsActive, cancellationToken);

        if (!categoryExists)
            throw new Exception("Category not found.");

        var product = new Product(
            command.Name,
            command.Description,
            command.Price,
            command.Stock,
            command.CategoryId);

        _context.Products.Add(product);

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}