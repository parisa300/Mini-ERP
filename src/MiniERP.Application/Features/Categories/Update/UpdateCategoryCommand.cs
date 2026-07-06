namespace MiniERP.Application.Features.Categories.Update;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? Description);