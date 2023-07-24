using System.ComponentModel.DataAnnotations;

namespace backend.API.DTOs
{
  public class LoginDto
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(12)]
    public string Password { get; set; }
  }
}