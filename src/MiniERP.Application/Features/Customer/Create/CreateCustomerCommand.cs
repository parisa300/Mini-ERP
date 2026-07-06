namespace MiniERP.Application.Features.Customers.Create;

public sealed record CreateCustomerCommand(
    string FullName,
    string PhoneNumber,
    string? Email,
    string Address);