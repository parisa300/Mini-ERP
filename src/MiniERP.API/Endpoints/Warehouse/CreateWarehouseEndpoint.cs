using MiniERP.API.Filters;
using MiniERP.Application.Features.Warehouses.Create;

namespace MiniERP.API.Endpoints.Warehouses;

public static class CreateWarehouseEndpoint
{
    public static IEndpointRouteBuilder MapCreateWarehouseEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/warehouses",
            async (
                CreateWarehouseCommand command,
                CreateWarehouseHandler handler,
                CancellationToken cancellationToken) =>
            {
                var id = await handler.Handle(
                    command,
                    cancellationToken);

                return Results.Created($"/warehouses/{id}", id);
            })
            .AddEndpointFilter<ValidationFilter<CreateWarehouseCommand>>();

        return app;
    }
}