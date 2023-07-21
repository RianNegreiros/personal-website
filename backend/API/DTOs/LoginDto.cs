using System.ComponentModel.DataAnnotations;

namespace backend.API.DTOs
{
  public class LoginDto
  {
    [Required]
    [MinLength(4)]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
  }
}