using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;

namespace MiniERP.Application.Features.Categories.Update;

public class UpdateCategoryHandler
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (category is null)
            return false;

        category.Update(command.Name, command.Description);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}