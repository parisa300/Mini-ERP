namespace MiniERP.Application.Features.Products.GetById;

public sealed record ProductDetailsDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    Guid CategoryId,
    string CategoryName);