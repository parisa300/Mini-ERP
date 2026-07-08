namespace MiniERP.Application.Features.Suppliers.GetAll;

public sealed record SupplierDto(
    Guid Id,
    string Name,
    string? PhoneNumber,
    string? Email,
    string? Address);