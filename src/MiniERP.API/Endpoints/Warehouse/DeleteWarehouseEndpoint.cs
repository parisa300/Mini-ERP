using MiniERP.Application.Features.Warehouses.Delete;

namespace MiniERP.API.Endpoints.Warehouses;

public static class DeleteWarehouseEndpoint
{
    public static IEndpointRouteBuilder MapDeleteWarehouseEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapDelete("/warehouses/{id:guid}",
            async (
                Guid id,
                DeleteWarehouseHandler handler,
                CancellationToken cancellationToken) =>
            {
                await handler.Handle(
                    new DeleteWarehouseCommand(id),
                    cancellationToken);

                return Results.NoContent();
            });

        return app;
    }
}