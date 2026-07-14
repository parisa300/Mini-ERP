using System.Net.Http.Json;
using MiniERP.Blazor.Models;

namespace MiniERP.Blazor.Services;

public class DashboardService
{
    private readonly HttpClient _httpClient;

    public DashboardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DashboardDto?> GetDashboardAsync()
    {
        var result = await _httpClient.GetAsync("dashboard");

        Console.WriteLine(result.StatusCode);

        return await result.Content.ReadFromJsonAsync<DashboardDto>();
    }
}