using MiniERP.Application.Features.Inventory.GetTransactions;

namespace MiniERP.API.Endpoints.Inventory;

public static class GetInventoryTransactionsEndpoint
{
    public static IEndpointRouteBuilder MapGetInventoryTransactionsEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/inventory/{inventoryId:guid}/transactions",
            async (
                Guid inventoryId,
                GetInventoryTransactionsHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(
                    new GetInventoryTransactionsQuery(inventoryId),
                    cancellationToken);

                return Results.Ok(result);
            });

        return app;
    }
}