

using MiniERP.Blazor.Models;

namespace MiniERP.Blazor.Services;

public class SalesOrderService : BaseApiService
{
    public SalesOrderService(HttpClient http)
        : base(http)
    {
    }

    public async Task CreateAsync(CreateSalesOrderRequest request)
    {
        await PostAsync("sales-orders", request);
    }
}