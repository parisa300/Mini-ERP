using MiniERP.API.Filters;
using MiniERP.Application.Features.Suppliers.Update;

namespace MiniERP.API.Endpoints.Suppliers;

public static class UpdateSupplierEndpoint
{
    public static IEndpointRouteBuilder MapUpdateSupplierEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPut("/suppliers/{id:guid}",
            async (
                Guid id,
                UpdateSupplierCommand command,
                UpdateSupplierHandler handler,
                CancellationToken cancellationToken) =>
            {
                await handler.Handle(
                    command with { Id = id },
                    cancellationToken);

                return Results.NoContent();
            })
            .AddEndpointFilter<ValidationFilter<UpdateSupplierCommand>>();

        return app;
    }
}