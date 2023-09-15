using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class RegisterInputModel : BaseAuthModel
{
  [Required]
  [MinLength(4)]
  [SwaggerSchema(Description = "Username must be at least 4 characters long")]
  public string Username { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [Compare(nameof(Password))]
  [SwaggerSchema(Description = "Confirm password must match password")]
  public string ConfirmPassword { get; set; }
}