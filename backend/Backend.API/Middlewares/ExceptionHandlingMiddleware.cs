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

    private static readonly Dictionary<Type, HttpStatusCode> ExceptionStatusCodes = new()
    {
        { typeof(PostNotFoundException), HttpStatusCode.NotFound },
        { typeof(AuthorizationException), HttpStatusCode.Unauthorized },
        { typeof(ImageUploadException), HttpStatusCode.BadRequest },
        { typeof(PostAlreadyExistsException), HttpStatusCode.BadRequest }
    };

    private static HttpStatusCode GetStatusCodeForException(Exception exception)
    {
        return ExceptionStatusCodes.TryGetValue(exception.GetType(), out HttpStatusCode statusCode)
            ? statusCode
            : HttpStatusCode.InternalServerError;
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ErrorResponse response = new()
        {
            Message = exception.Message
        };

        context.Response.StatusCode = (int)GetStatusCodeForException(exception);
        response.Message = exception.Message;

        context.Response.ContentType = "application/json";
        string result = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(result);
    }
}

public class ErrorResponse
{
    public string Message { get; set; } = "An error occurred while processing the request.";
}