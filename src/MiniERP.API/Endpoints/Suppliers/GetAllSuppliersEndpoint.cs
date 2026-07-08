using MiniERP.Application.Features.Suppliers.GetAll;

namespace MiniERP.API.Endpoints.Suppliers;

public static class GetAllSuppliersEndpoint
{
    public static IEndpointRouteBuilder MapGetAllSuppliersEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/suppliers",
            async (
                int page,
                int pageSize,
                GetAllSuppliersHandler handler,
                CancellationToken cancellationToken) =>
            {
                var result = await handler.Handle(
                    new GetAllSuppliersQuery(page, pageSize),
                    cancellationToken);

                return Results.Ok(result);
            });

        return app;
    }
}