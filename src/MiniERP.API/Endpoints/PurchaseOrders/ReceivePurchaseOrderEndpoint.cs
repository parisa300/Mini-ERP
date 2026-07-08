using MiniERP.Application.Features.PurchaseOrders.Receive;

namespace MiniERP.API.Endpoints.PurchaseOrders;

public static class ReceivePurchaseOrderEndpoint
{
    public static IEndpointRouteBuilder MapReceivePurchaseOrderEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/purchase-orders/{id:guid}/receive",
            async (
                Guid id,
                ReceivePurchaseOrderHandler handler,
                CancellationToken cancellationToken) =>
            {
                await handler.Handle(
                    new ReceivePurchaseOrderCommand(id),
                    cancellationToken);

                return Results.NoContent();
            });

        return app;
    }
}