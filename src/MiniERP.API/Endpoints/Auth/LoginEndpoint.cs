using MiniERP.API.Filters;
using MiniERP.Application.Features.Auth.Login;

namespace MiniERP.API.Endpoints.Auth;

public static class LoginEndpoint
{
    public static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login",
        async (
            LoginCommand command,
            LoginHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(
                command,
                cancellationToken);

            return Results.Ok(response);
        })
        .AddEndpointFilter<ValidationFilter<LoginCommand>>();

        return app;
    }
}