using MiniERP.API.Filters;
using MiniERP.Application.Features.Warehouses.Update;

namespace MiniERP.API.Endpoints.Warehouses;

public static class UpdateWarehouseEndpoint
{
    public static IEndpointRouteBuilder MapUpdateWarehouseEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPut("/warehouses/{id:guid}",
            async (
                Guid id,
                UpdateWarehouseCommand command,
                UpdateWarehouseHandler handler,
                CancellationToken cancellationToken) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("Route id and body id do not match.");

                await handler.Handle(command, cancellationToken);

                return Results.NoContent();
            })
            .AddEndpointFilter<ValidationFilter<UpdateWarehouseCommand>>();

        return app;
    }
}