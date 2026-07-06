using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Features.Categories.DTOs;

namespace MiniERP.Application.Features.Categories.GetAll;

public class GetAllCategoriesHandler
{
    private readonly IApplicationDbContext _context;

    public GetAllCategoriesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> Handle(CancellationToken cancellationToken)
    {
        return await _context.Categories
    .Where(x => x.IsActive)
            .AsNoTracking()
            .Select(x => new CategoryDto(
                x.Id,
                x.Name,
                x.Description,
                x.IsActive))
            .ToListAsync(cancellationToken);
    }
}