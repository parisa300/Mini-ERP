using FluentValidation;

namespace MiniERP.API.Filters;

public class ValidationFilter<T> : IEndpointFilter
    where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices
            .GetService<IValidator<T>>();

        if (validator is not null)
        {
            var model = context.Arguments
                .OfType<T>()
                .First();

            await validator.ValidateAndThrowAsync(model);
        }

        return await next(context);
    }
}