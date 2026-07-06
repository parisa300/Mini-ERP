using MiniERP.API.Filters;
using MiniERP.Application.Features.Customers.Create;

namespace MiniERP.API.Endpoints.Customers;

public static class CreateCustomerEndpoint
{
    public static IEndpointRouteBuilder MapCreateCustomerEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/customers",
            async (
                CreateCustomerCommand command,
                CreateCustomerHandler handler,
                CancellationToken cancellationToken) =>
            {
                var id = await handler.Handle(
                    command,
                    cancellationToken);

                return Results.Created($"/customers/{id}", id);
            })
            .AddEndpointFilter<ValidationFilter<CreateCustomerCommand>>();
          //  .RequireAuthorization();

        return app;
    }
}