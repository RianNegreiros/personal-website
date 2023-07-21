using System.ComponentModel.DataAnnotations;

namespace backend.API.DTOs;

public class RegisterDto
{
    [Required]
    [MinLength(4)]
    public string Username { get; }
        
    [Required]
    public string Password { get; }
}