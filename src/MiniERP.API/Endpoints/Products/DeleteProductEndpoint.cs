using MiniERP.Application.Features.Products.Delete;

namespace MiniERP.API.Endpoints.Products;

public static class DeleteProductEndpoint
{
    public static IEndpointRouteBuilder MapDeleteProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}",
            async (
                Guid id,
                DeleteProductHandler handler,
                CancellationToken cancellationToken) =>
            {
                var deleted = await handler.Handle(
                    new DeleteProductCommand(id),
                    cancellationToken);

                return deleted
                    ? Results.NoContent()
                    : Results.NotFound();
            });

        return app;
    }
}