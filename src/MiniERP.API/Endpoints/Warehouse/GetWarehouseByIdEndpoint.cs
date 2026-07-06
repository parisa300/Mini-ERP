using MiniERP.Application.Features.Warehouses.GetById;

namespace MiniERP.API.Endpoints.Warehouses;

public static class GetWarehouseByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetWarehouseByIdEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/warehouses/{id:guid}",
            async (
                Guid id,
                GetWarehouseByIdHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(
                    new GetWarehouseByIdQuery(id),
                    cancellationToken);

                return Results.Ok(result);
            });

        return app;
    }
}