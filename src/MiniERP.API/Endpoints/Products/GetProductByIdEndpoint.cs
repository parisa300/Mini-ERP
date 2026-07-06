using MiniERP.Application.Features.Products.GetById;

namespace MiniERP.API.Endpoints.Products;

public static class GetProductByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetProductByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}",
            async (
                Guid id,
                GetProductByIdHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(
                    new GetProductByIdQuery(id),
                    cancellationToken);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

        return app;
    }
}