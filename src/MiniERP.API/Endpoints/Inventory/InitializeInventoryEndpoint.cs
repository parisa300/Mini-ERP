using MiniERP.API.Filters;
using MiniERP.Application.Features.Inventory.Initialize;

namespace MiniERP.API.Endpoints.Inventory;

public static class InitializeInventoryEndpoint
{
    public static IEndpointRouteBuilder MapInitializeInventoryEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/inventory/initialize",
            async (
                InitializeInventoryCommand command,
                InitializeInventoryHandler handler,
                CancellationToken cancellationToken) =>
            {
                var id = await handler.Handle(command, cancellationToken);

                return Results.Ok(new { Id = id });
            })
            .AddEndpointFilter<ValidationFilter<InitializeInventoryCommand>>();

        return app;
    }
}