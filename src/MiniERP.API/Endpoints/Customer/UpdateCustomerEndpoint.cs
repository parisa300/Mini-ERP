using FluentValidation;
using MiniERP.API.Filters;
using MiniERP.Application.Features.Customers.Update;

namespace MiniERP.API.Endpoints.Customers;

public static class UpdateCustomerEndpoint
{
    public static IEndpointRouteBuilder MapUpdateCustomerEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPut("/customers/{id:guid}",
            async (
                Guid id,
                UpdateCustomerCommand command,
                UpdateCustomerHandler handler,
                CancellationToken cancellationToken) =>
            {
                command = command with { Id = id };

                await handler.Handle(command, cancellationToken);

                return Results.NoContent();
            })
            .AddEndpointFilter<ValidationFilter<UpdateCustomerCommand>>();

        return app;
    }
}