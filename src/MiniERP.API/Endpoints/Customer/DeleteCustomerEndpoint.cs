using MiniERP.Application.Features.Customers.Delete;

namespace MiniERP.API.Endpoints.Customers;

public static class DeleteCustomerEndpoint
{
    public static IEndpointRouteBuilder MapDeleteCustomerEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapDelete("/customers/{id:guid}",
            async (
                Guid id,
                DeleteCustomerHandler handler,
                CancellationToken cancellationToken) =>
            {
                await handler.Handle(
                    new DeleteCustomerCommand(id),
                    cancellationToken);

                return Results.NoContent();
            });

        return app;
    }
}