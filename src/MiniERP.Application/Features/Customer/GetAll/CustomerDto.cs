namespace MiniERP.Application.Features.Customers.GetAll;

public sealed record CustomerDto(
    Guid Id,
    string FullName,
    string PhoneNumber,
    string? Email,
    string Address);