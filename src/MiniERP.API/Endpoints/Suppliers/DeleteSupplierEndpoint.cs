using MiniERP.Application.Features.Suppliers.Delete;

namespace MiniERP.API.Endpoints.Suppliers;

public static class DeleteSupplierEndpoint
{
    public static IEndpointRouteBuilder MapDeleteSupplierEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapDelete("/suppliers/{id:guid}",
            async (
                Guid id,
                DeleteSupplierHandler handler,
                CancellationToken cancellationToken) =>
            {
                await handler.Handle(
                    new DeleteSupplierCommand(id),
                    cancellationToken);

                return Results.NoContent();
            });

        return app;
    }
}