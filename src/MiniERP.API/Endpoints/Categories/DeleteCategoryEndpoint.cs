using MiniERP.Application.Features.Categories.Delete;

namespace MiniERP.API.Endpoints.Categories;

public static class DeleteCategoryEndpoint
{
    public static IEndpointRouteBuilder MapDeleteCategoryEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/categories/{id:guid}",
            async (
                Guid id,
                DeleteCategoryHandler handler,
                CancellationToken cancellationToken) =>
            {
                var deleted = await handler.Handle(
                    new DeleteCategoryCommand(id),
                    cancellationToken);

                return deleted
                    ? Results.NoContent()
                    : Results.NotFound();
            });

        return app;
    }
}