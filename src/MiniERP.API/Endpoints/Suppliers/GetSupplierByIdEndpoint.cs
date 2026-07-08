using MiniERP.Application.Features.Suppliers.GetById;

namespace MiniERP.API.Endpoints.Suppliers;

public static class GetSupplierByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetSupplierByIdEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/suppliers/{id:guid}",
            async (
                Guid id,
                GetSupplierByIdHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(
                    new GetSupplierByIdQuery(id),
                    cancellationToken);

                return Results.Ok(result);
            });

        return app;
    }
}