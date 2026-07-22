using MiniERP.Blazor.Services;

namespace MiniERP.Blazor.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        var api = new Uri("http://localhost:5172/");

        services.AddHttpClient<ProductService>(x =>
            x.BaseAddress = api);

        services.AddHttpClient<CustomerService>(x =>
            x.BaseAddress = api);

        services.AddHttpClient<WarehouseService>(x =>
            x.BaseAddress = api);

        services.AddHttpClient<SalesOrderService>(x =>
            x.BaseAddress = api);

        services.AddHttpClient<DashboardService>(x =>
            x.BaseAddress = api);

        services.AddHttpClient<ChartService>(x =>
            x.BaseAddress = api);

        return services;
    }
}