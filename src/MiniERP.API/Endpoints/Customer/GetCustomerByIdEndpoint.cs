using MiniERP.Application.Features.Customers.GetById;

namespace MiniERP.API.Endpoints.Customers;

public static class GetCustomerByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetCustomerByIdEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/customers/{id:guid}",
            async (
                Guid id,
                GetCustomerByIdHandler handler,
                CancellationToken cancellationToken) =>
            {
                var customer = await handler.Handle(
                    new GetCustomerByIdQuery(id),
                    cancellationToken);

                if (customer is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(customer);
            });

        return app;
    }
}