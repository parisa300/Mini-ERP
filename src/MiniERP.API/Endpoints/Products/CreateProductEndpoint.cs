using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MiniERP.API.Filters;
using MiniERP.Application.Features.Products.Create;

namespace MiniERP.API.Endpoints.Products;

public static class CreateProductEndpoint
{
    public static IEndpointRouteBuilder MapCreateProductEndpoint(this IEndpointRouteBuilder app)
    {
app.MapPost("/products",
async(
CreateProductCommand command,
CreateProductHandler handler,
CancellationToken cancellationToken)=>
{
    var id = await handler.Handle(command,cancellationToken);

    return Results.Created($"/products/{id}",id);
})
.AddEndpointFilter<ValidationFilter<CreateProductCommand>>();

        return app;
    }
}