using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Models;

public class RegisterInputModel
{
  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [MinLength(4)]
  public string Username { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [MinLength(12)]
  public string Password { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [Compare(nameof(Password))]
  public string ConfirmPassword { get; set; }
}