using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models.InputModels;

public class LoginInputModel : BaseAuthModel
{
    [SwaggerSchema(Description = "Set to true to remember the user")]
    public bool RememberMe { get; set; }
}