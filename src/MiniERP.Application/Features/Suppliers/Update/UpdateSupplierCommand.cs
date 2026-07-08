namespace MiniERP.Application.Features.Suppliers.Update;

public sealed record UpdateSupplierCommand(
    Guid Id,
    string Name,
    string? PhoneNumber,
    string? Email,
    string? Address);