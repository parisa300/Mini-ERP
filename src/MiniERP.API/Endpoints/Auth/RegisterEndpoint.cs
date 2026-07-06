using FluentValidation;
using MiniERP.API.Filters;
using MiniERP.Application.Features.Auth.Register;

namespace MiniERP.API.Endpoints.Auth;

public static class RegisterEndpoint
{
    public static IEndpointRouteBuilder MapRegisterEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register",
        async (
            RegisterCommand command,
            RegisterHandler handler,
            CancellationToken cancellationToken) =>
        {
            var id = await handler.Handle(command, cancellationToken);

            return Results.Created($"/users/{id}", id);
        })
        .AddEndpointFilter<ValidationFilter<RegisterCommand>>();

        return app;
    }
}