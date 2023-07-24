using backend.API.Errors;

namespace backend.API.Middlewares;

public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;

  public ExceptionMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (CustomException ex)
    {
      context.Response.StatusCode = StatusCodes.Status400BadRequest;
      await context.Response.WriteAsync(ex.Message);
    }
    catch (Exception ex)
    {
      context.Response.StatusCode = StatusCodes.Status500InternalServerError;
      await context.Response.WriteAsync("An unexpected error occurred.");
    }
  }
}
