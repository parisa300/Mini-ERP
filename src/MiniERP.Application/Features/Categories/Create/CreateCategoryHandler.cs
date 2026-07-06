using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Features.Categories.Commands.CreateCategory;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Features.Categories.Create;

public class CreateCategoryHandler
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(
            request.Name,
            request.Description);

        _context.Categories.Add(category);

        await _context.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}