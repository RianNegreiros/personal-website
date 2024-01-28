using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models.InputModels;

public class AdminCreateUserInputModel
{
  [Required]
  [MinLength(4)]
  [SwaggerSchema(Description = "Username must be at least 4 characters long")]
  public string Username { get; set; }

  [Required]
  [EmailAddress]
  [SwaggerSchema(Description = "Email address")]
  public string Email { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [MinLength(12)]
  [SwaggerSchema(Description = "Password must be at least 12 characters long")]
  public string Password { get; set; }

  [SwaggerSchema(Description = "Set if user is admin")]
  public bool Admin { get; set; } = false;
}
