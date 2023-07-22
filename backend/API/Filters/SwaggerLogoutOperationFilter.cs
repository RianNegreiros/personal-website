using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Filters;

public class SwaggerLogoutOperationFilter : IOperationFilter
{
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
    var controllerName = controllerActionDescriptor?.ControllerName;
    var actionName = controllerActionDescriptor?.ActionName;

    if (controllerName == "UserController" && actionName == "Logout")
    {
      operation.OperationId = "LogoutUser";
      operation.Summary = "Logs out the currently authenticated user.";
      operation.Description = "This endpoint logs out the user and invalidates the authentication token.";
    }
  }
}
