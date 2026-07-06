namespace MiniERP.Application.Features.Warehouses.GetAll;

public sealed record GetAllWarehousesQuery(
    string? Search,
    int Page = 1,
    int PageSize = 10);