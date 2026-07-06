namespace MiniERP.Application.Features.Customers.Update;

public sealed record UpdateCustomerCommand(
    Guid Id,
    string FullName,
    string PhoneNumber,
    string? Email,
    string Address);