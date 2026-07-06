using MiniERP.Application.Features.Categories.GetById;

namespace MiniERP.API.Endpoints.Categories;

public static class GetCategoryByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetCategoryByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/categories/{id:guid}",
            async (
                Guid id,
                GetCategoryByIdHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(
                    new GetCategoryByIdQuery(id),
                    cancellationToken);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });

        return app;
    }
}