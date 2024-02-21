using System.ComponentModel.DataAnnotations;

using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class BaseAuthModel
{
    [Required]
    [EmailAddress]
    [SwaggerSchema(Description = "Email address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(12)]
    [SwaggerSchema(Description = "Password must be at least 12 characters long")]
    public string Password { get; set; }
}