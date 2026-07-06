using FluentValidation;

namespace MiniERP.API.Extensions;

public static class ValidationExtensions
{
    public static async Task ValidateAsync<T>(
        this IValidator<T> validator,
        T model)
    {
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}