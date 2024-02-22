using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Models.InputModels;

public class SubscriberInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}