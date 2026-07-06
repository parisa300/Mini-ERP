using Microsoft.AspNetCore.Mvc;
using MiniERP.Application.Features.Products.Update;

namespace MiniERP.API.Endpoints.Products;

public static class UpdateProductEndpoint
{
    public static IEndpointRouteBuilder MapUpdateProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}",
            async (
                Guid id,
                [FromBody] UpdateProductCommand command,
                UpdateProductHandler handler,
                CancellationToken cancellationToken) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("Id mismatch.");

                var updated = await handler.Handle(command, cancellationToken);

                return updated
                    ? Results.NoContent()
                    : Results.NotFound();
            });

        return app;
    }
}