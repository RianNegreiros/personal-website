using System.ComponentModel.DataAnnotations;

namespace backend.API.DTOs;

public class RegisterDto
{
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