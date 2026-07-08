namespace MiniERP.Application.Features.Suppliers.Create;

public sealed record CreateSupplierCommand(
    string Name,
    string? PhoneNumber,
    string? Email,
    string? Address);