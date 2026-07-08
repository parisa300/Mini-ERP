using MiniERP.API.Filters;
using MiniERP.Application.Features.Suppliers.Create;

namespace MiniERP.API.Endpoints.Suppliers;

public static class CreateSupplierEndpoint
{
    public static IEndpointRouteBuilder MapCreateSupplierEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/suppliers",
            async (
                CreateSupplierCommand command,
                CreateSupplierHandler handler,
                CancellationToken cancellationToken) =>
            {
                var id = await handler.Handle(command, cancellationToken);

                return Results.Ok(new { Id = id });
            })
            .AddEndpointFilter<ValidationFilter<CreateSupplierCommand>>();

        return app;
    }
}