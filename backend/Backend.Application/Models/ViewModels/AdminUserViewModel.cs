using Backend.Core.Models;

using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models.ViewModels;

public class AdminUserViewModel
{
    [SwaggerSchema(Description = "User Id")]
    public string Id { get; set; }

    [SwaggerSchema(Description = "User Email")]
    public string Email { get; set; }

    [SwaggerSchema(Description = "User Username")]
    public string Username { get; set; }

    [SwaggerSchema(Description = "Is user admin")]
    public bool IsAdmin { get; set; }

    [SwaggerSchema(Description = "User posts")]
    public List<Post> Posts { get; set; }

    [SwaggerSchema(Description = "User comments")]
    public List<Comment> Comments { get; set; }
}