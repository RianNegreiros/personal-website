using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Models;

public class LoginInputModel
{
  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [MinLength(12)]
  public string Password { get; set; }
  public bool RememberMe { get; set; }
}