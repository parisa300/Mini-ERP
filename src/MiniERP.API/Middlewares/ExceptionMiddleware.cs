using System.Text.Json;
using FluentValidation;
using MiniERP.Application.Common.Exceptions;

namespace MiniERP.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                errors
            }));
        }
        catch (NotFoundException ex)
        {
            await WriteResponse(
                context,
                StatusCodes.Status404NotFound,
                ex.Message);
        }
        catch (ConflictException ex)
        {
            await WriteResponse(
                context,
                StatusCodes.Status409Conflict,
                ex.Message);
        }
        catch (BusinessRuleException ex)
        {
            await WriteResponse(
                context,
                StatusCodes.Status400BadRequest,
                ex.Message);
        }
        catch (Exception ex)
        {
            await WriteResponse(
                context,
                StatusCodes.Status500InternalServerError,
                ex.Message);
        }
    }

    private static async Task WriteResponse(
        HttpContext context,
        int statusCode,
        string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(new
            {
                message
            }));
    }
}