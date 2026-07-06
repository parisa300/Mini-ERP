using Microsoft.AspNetCore.Mvc;
using MiniERP.Application.Features.Warehouses.GetAll;

namespace MiniERP.API.Endpoints.Warehouses;

public static class GetAllWarehousesEndpoint
{
    public static IEndpointRouteBuilder MapGetAllWarehousesEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/warehouses",
            async (
                [AsParameters] GetAllWarehousesQuery query,
                GetAllWarehousesHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(query, cancellationToken);

                return Results.Ok(result);
            });

        return app;
    }
}