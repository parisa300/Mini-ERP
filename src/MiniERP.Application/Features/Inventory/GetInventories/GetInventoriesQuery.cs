public sealed record GetInventoriesQuery(
    int Page = 1,
    int PageSize = 20,
    string? Search = null,
    Guid? WarehouseId = null);