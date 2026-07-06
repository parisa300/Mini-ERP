namespace MiniERP.Application.Features.Products.GetAll;

public sealed record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    string CategoryName);