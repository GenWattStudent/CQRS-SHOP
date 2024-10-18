using CarShop.Application.Dtos;
using CarShop.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.API.Middlewares;

public class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            var validationsErrors = new List<ValidationDTO>();

            foreach (var error in exception.Errors)
            {
                validationsErrors.Add(new ValidationDTO
                {
                    Property = error.PropertyName,
                    Message = error.ErrorMessage
                });
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(validationsErrors);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request",
                Detail = e.Message
            });
        }
    }
}
