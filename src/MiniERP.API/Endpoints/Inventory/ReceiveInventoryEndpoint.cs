using MiniERP.API.Filters;
using MiniERP.Application.Features.Inventory.Receive;

namespace MiniERP.API.Endpoints.Inventory;

public static class ReceiveInventoryEndpoint
{
    public static IEndpointRouteBuilder MapReceiveInventoryEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/inventory/receive",
            async (
                ReceiveInventoryCommand command,
                ReceiveInventoryHandler handler,
                CancellationToken cancellationToken) =>
            {
                await handler.Handle(command, cancellationToken);

                return Results.Ok(new
                {
                    Message = "Inventory updated successfully."
                });
            })
            .AddEndpointFilter<ValidationFilter<ReceiveInventoryCommand>>();

        return app;
    }
}