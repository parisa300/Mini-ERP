using MiniERP.Application.Features.Products.GetAll;
using MiniERP.Shared.Models;

namespace MiniERP.API.Endpoints.Products;

public static class GetAllProductsEndpoint
{
    public static IEndpointRouteBuilder MapGetAllProductsEndpoint(this IEndpointRouteBuilder app)
    {
  app.MapGet("/products",
async(
    string? search,
    Guid? categoryId,
    decimal? minPrice,
    decimal? maxPrice,
    string? sortBy,
    int page,
    int pageSize,
    GetAllProductsHandler handler,
    CancellationToken cancellationToken)=>
{
    var result = await handler.Handle(
        new GetAllProductsQuery(
            new PaginationRequest
            {
                Page = page,
                PageSize = pageSize
            },
            new ProductFilter
            {
                Search = search,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SortBy = sortBy
            }),
        cancellationToken);

    return Results.Ok(result);
});

        return app;
    }
}