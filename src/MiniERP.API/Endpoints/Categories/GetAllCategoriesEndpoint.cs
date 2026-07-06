using MiniERP.Application.Features.Categories.GetAll;

namespace MiniERP.API.Endpoints.Categories;

public static class GetAllCategoriesEndpoint
{
    public static IEndpointRouteBuilder MapGetAllCategoriesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/categories",
            async (
                GetAllCategoriesHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(cancellationToken);

                return Results.Ok(result);
            });

        return app;
    }
}