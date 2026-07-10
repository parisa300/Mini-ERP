using MiniERP.Application.Features.Dashboard;

namespace MiniERP.API.Endpoints.Dashboard;

public static class GetDashboardEndpoint
{
    public static IEndpointRouteBuilder MapGetDashboardEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/dashboard", async (
            GetDashboardHandler handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(
                new GetDashboardQuery(),
                cancellationToken);

            return Results.Ok(result);
        })
        .WithTags("Dashboard")
        .WithOpenApi();

        return app;
    }
}