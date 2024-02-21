using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models.ViewModels;

public class UserViewModel
{
    [SwaggerSchema(Description = "User Id")]
    public string Id { get; set; }

    [SwaggerSchema(Description = "User Email")]
    public string Email { get; set; }

    [SwaggerSchema(Description = "User Username")]
    public string Username { get; set; }

    [SwaggerSchema(Description = "User Token")]
    public string Token { get; set; }

    [SwaggerSchema(Description = "Is user admin")]
    public bool IsAdmin { get; set; }

    [SwaggerSchema(Description = "Remember user")]
    public bool RememberMe { get; set; }
}