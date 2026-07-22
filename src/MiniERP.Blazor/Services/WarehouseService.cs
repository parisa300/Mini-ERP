using MiniERP.Blazor.Models;


namespace MiniERP.Blazor.Services;

public class WarehouseService : BaseApiService
{
    public WarehouseService(HttpClient http)
        : base(http)
    {
    }

    public async Task<PagedResult<WarehouseDto>> GetWarehousesAsync(
        int page = 1,
        int pageSize = 10,
        string? search = null)
    {
        return await GetAsync<PagedResult<WarehouseDto>>
        (
            $"warehouses?page={page}&pageSize={pageSize}&search={search}"
        )
        ?? new();
    }
}