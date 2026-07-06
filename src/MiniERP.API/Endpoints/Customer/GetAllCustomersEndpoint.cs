using Microsoft.AspNetCore.Mvc;
using MiniERP.Application.Features.Customers.GetAll;

namespace MiniERP.API.Endpoints.Customers;

public static class GetAllCustomersEndpoint
{
    public static IEndpointRouteBuilder MapGetAllCustomersEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/customers",
            async (
                [FromQuery] string? search,
                [FromQuery] string? sortBy,
                [FromQuery] int page,
                [FromQuery] int pageSize,
                GetAllCustomersHandler handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetAllCustomersQuery(
                    search,
                    sortBy,
                    page == 0 ? 1 : page,
                    pageSize == 0 ? 10 : pageSize);

                var result = await handler.Handle(query, cancellationToken);

                return Results.Ok(result);
            });

        return app;
    }
}