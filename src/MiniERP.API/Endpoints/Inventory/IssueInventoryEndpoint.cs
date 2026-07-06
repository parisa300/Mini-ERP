using MiniERP.API.Filters;
using MiniERP.Application.Features.Inventory.Issue;


namespace MiniERP.API.Endpoints.Inventory;

public static class IssueInventoryEndpoint
{
    public static IEndpointRouteBuilder MapIssueInventoryEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/inventory/issue",
            async (
                IssueInventoryCommand command,
                IssueInventoryHandler handler,
                CancellationToken cancellationToken) =>
            {
                await handler.Handle(command, cancellationToken);

                return Results.Ok(new
                {
                    Message = "Inventory updated successfully."
                });
            })
            .AddEndpointFilter<ValidationFilter<IssueInventoryCommand>>();

        return app;
    }
}