using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Categories.Delete;

public class DeleteCategoryHandler
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        DeleteCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (category is null)
            return false;

        category.Deactivate();

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}