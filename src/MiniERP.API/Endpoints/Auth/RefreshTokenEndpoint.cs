using MiniERP.Application.Features.Auth.RefreshToken;

namespace MiniERP.API.Endpoints.Auth;

public static class RefreshTokenEndpoint
{
    public static IEndpointRouteBuilder MapRefreshTokenEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost(
            "/auth/refresh-token",
            async (
                RefreshTokenCommand command,
                RefreshTokenHandler handler,
                CancellationToken cancellationToken) =>
            {
                var response = await handler.Handle(
                    command,
                    cancellationToken);

                return Results.Ok(response);
            });

        return app;
    }
}