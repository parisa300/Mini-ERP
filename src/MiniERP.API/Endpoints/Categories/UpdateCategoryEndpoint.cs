using Microsoft.AspNetCore.Mvc;
using MiniERP.Application.Features.Categories.Update;

namespace MiniERP.API.Endpoints.Categories;

public static class UpdateCategoryEndpoint
{
    public static IEndpointRouteBuilder MapUpdateCategoryEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/categories/{id:guid}",
            async (
                Guid id,
                [FromBody] UpdateCategoryCommand request,
                UpdateCategoryHandler handler,
                CancellationToken cancellationToken) =>
            {
                var command = request with { Id = id };

                var updated = await handler.Handle(command, cancellationToken);

                return updated
                    ? Results.NoContent()
                    : Results.NotFound();
            });

        return app;
    }
}