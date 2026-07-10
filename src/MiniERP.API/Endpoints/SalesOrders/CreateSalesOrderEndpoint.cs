using MiniERP.API.Filters;
using MiniERP.Application.Features.SalesOrders.Create;

namespace MiniERP.API.Endpoints.SalesOrders;

public static class CreateSalesOrderEndpoint
{
    public static IEndpointRouteBuilder MapCreateSalesOrderEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/sales-orders",
            async (
                CreateSalesOrderCommand command,
                CreateSalesOrderHandler handler,
                CancellationToken cancellationToken) =>
            {
                var id = await handler.Handle(
                    command,
                    cancellationToken);

                return Results.Ok(new
                {
                    id
                });
            })
            .AddEndpointFilter<ValidationFilter<CreateSalesOrderCommand>>();

        return app;
    }
}