using Microsoft.AspNetCore.Mvc;
using MiniERP.Application.Features.Inventory.GetInventories;

namespace MiniERP.API.Endpoints.Inventory;

public static class GetInventoriesEndpoint
{
    public static IEndpointRouteBuilder MapGetInventoriesEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/inventory",
            async (
                [AsParameters] GetInventoriesQuery query,
                [FromServices] GetInventoriesHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(query, cancellationToken);

                return Results.Ok(result);
            });

        return app;
    }
}