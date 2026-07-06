using Microsoft.EntityFrameworkCore;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.Features.Categories.DTOs;

namespace MiniERP.Application.Features.Categories.GetById;

public class GetCategoryByIdHandler
{
    private readonly IApplicationDbContext _context;

    public GetCategoryByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto?> Handle(
        GetCategoryByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(x => x.Id == query.Id && x.IsActive)
            .Select(x => new CategoryDto(
                x.Id,
                x.Name,
                x.Description,
                x.IsActive))
            .FirstOrDefaultAsync(cancellationToken);
    }
}