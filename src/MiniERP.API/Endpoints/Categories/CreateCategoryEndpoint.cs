using Microsoft.AspNetCore.Mvc;
using MiniERP.Application.Features.Categories.Commands.CreateCategory;
using MiniERP.Application.Features.Categories.Create;

namespace MiniERP.API.Endpoints.Categories;

public static class CreateCategoryEndpoint
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/categories",
            async (
                [FromBody] CreateCategoryCommand command,
                CreateCategoryHandler handler,
                CancellationToken cancellationToken) =>
            {
                var id = await handler.Handle(command, cancellationToken);

                return Results.Created($"/categories/{id}", id);
            });

        return app;
    }
}