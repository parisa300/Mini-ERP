using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Products.Delete;

public class DeleteProductHandler
{
    private readonly IApplicationDbContext _context;

    public DeleteProductHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.IsActive,
                cancellationToken);

        if (product is null)
            return false;

        product.Deactivate();

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}