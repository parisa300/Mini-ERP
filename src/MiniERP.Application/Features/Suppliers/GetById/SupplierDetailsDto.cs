namespace MiniERP.Application.Features.Suppliers.GetById;

public sealed record SupplierDetailsDto(
    Guid Id,
    string Name,
    string? PhoneNumber,
    string? Email,
    string? Address);