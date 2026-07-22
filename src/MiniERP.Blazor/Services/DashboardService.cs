using MiniERP.Blazor.Models;

namespace MiniERP.Blazor.Services;

public class DashboardService : BaseApiService
{
    public DashboardService(HttpClient http)
        : base(http)
    {
    }

    public async Task<DashboardDto?> GetDashboardAsync()
    {
        return await GetAsync<DashboardDto>("dashboard");
    }
}