using System.Net.Http.Json;
using MiniERP.Blazor.Models;

namespace MiniERP.Blazor.Services;

public class SalesOrderService
{
    private readonly HttpClient _http;

    public SalesOrderService(HttpClient http)
    {
        _http = http;
    }

    public async Task CreateAsync(CreateSalesOrderRequest request)
    {
        var response = await _http.PostAsJsonAsync(
            "sales-orders",
            request);

        response.EnsureSuccessStatusCode();
    }
}