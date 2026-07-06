namespace MiniERP.Application.Features.Customers.GetAll;

public sealed record GetAllCustomersQuery(
    string? Search,
    string? SortBy,
    int Page = 1,
    int PageSize = 10);