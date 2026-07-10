using MiniERP.Application.Features.SalesOrders.Confirm;

namespace MiniERP.API.Endpoints.SalesOrders;

public static class ConfirmSalesOrderEndpoint
{
    public static IEndpointRouteBuilder MapConfirmSalesOrderEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/sales-orders/{id:guid}/confirm",
            async (
                Guid id,
                ConfirmSalesOrderHandler handler,
                CancellationToken cancellationToken) =>
            {
                await handler.Handle(
                    new ConfirmSalesOrderCommand(id),
                    cancellationToken);

                return Results.NoContent();
            });

        return app;
    }
}