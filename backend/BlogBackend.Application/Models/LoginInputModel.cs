using System.ComponentModel.DataAnnotations;

namespace BlogBackend.Application.Models;

public class LoginInputModel
{
  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [MinLength(12)]
  public string Password { get; set; }
}