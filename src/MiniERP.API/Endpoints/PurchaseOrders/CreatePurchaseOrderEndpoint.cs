using MiniERP.API.Filters;
using MiniERP.Application.Features.PurchaseOrders.Create;

namespace MiniERP.API.Endpoints.PurchaseOrders;

public static class CreatePurchaseOrderEndpoint
{
    public static IEndpointRouteBuilder MapCreatePurchaseOrderEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/purchase-orders",
            async (
                CreatePurchaseOrderCommand command,
                CreatePurchaseOrderHandler handler,
                CancellationToken cancellationToken) =>
            {
                var id = await handler.Handle(command, cancellationToken);

                return Results.Ok(new { Id = id });
            })
            .AddEndpointFilter<ValidationFilter<CreatePurchaseOrderCommand>>();

        return app;
    }
}