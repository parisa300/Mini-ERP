using System.Net.Http.Json;
using MiniERP.Blazor.Models;

namespace MiniERP.Blazor.Services;

public class WarehouseService
{
    private readonly HttpClient _http;

    public WarehouseService(HttpClient http)
    {
        _http = http;
    }

    public async Task<PagedResult<WarehouseDto>> GetWarehousesAsync()
    {
        return await _http.GetFromJsonAsync<PagedResult<WarehouseDto>>(
            "warehouses") ?? new();
    }
}