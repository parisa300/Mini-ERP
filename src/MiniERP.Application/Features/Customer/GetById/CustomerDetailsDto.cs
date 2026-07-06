namespace MiniERP.Application.Features.Customers.GetById;

public sealed record CustomerDetailsDto(
    Guid Id,
    string FullName,
    string PhoneNumber,
    string? Email,
    string Address);