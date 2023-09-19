using System.Net;
using System.Text.Json;
using Backend.Core.Exceptions;

namespace Backend.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ErrorResponse response = new();

        if (exception is PostNotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            response.Message = exception.Message;
        }
        else if (exception is AuthorizationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            response.Message = exception.Message;
        }
        else if (exception is ImageUploadException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.Message = exception.Message;
        }
        else if (exception is PostAlreadyExistsException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.Message = exception.Message;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        context.Response.ContentType = "application/json";
        string? result = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(result);
    }
}

public class ErrorResponse
{
    public string Message { get; set; } = "An error occurred while processing the request.";
}