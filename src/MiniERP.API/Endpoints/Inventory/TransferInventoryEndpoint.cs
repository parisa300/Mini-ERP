using Microsoft.AspNetCore.Mvc;
using MiniERP.API.Filters;
using MiniERP.Application.Features.Inventory.Transfer;

namespace MiniERP.API.Endpoints.Inventory;

public static class TransferInventoryEndpoint
{
    public static IEndpointRouteBuilder MapTransferInventoryEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/inventory/transfer",
            async (
                [FromBody] TransferInventoryCommand command,
                [FromServices] TransferInventoryHandler handler,
                CancellationToken cancellationToken) =>
            {
                await handler.Handle(command, cancellationToken);

                return Results.Ok();
            })
            .AddEndpointFilter<ValidationFilter<TransferInventoryCommand>>();

        return app;
    }
}