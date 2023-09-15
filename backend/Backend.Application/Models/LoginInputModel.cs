using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class LoginInputModel : AuthBaseModel
{
  [SwaggerSchema(Description = "Set to true to remember the user")]
  public bool RememberMe { get; set; }
}