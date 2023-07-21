using System.ComponentModel.DataAnnotations;

namespace backend.Core.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}