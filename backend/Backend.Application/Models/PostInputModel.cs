using System.ComponentModel.DataAnnotations;

namespace Backend.Application.Models;

public class PostInputModel
{
  public string AuthorId { get; set; }
  [Required]
  [MinLength(4)]
  public string Title { get; set; }

  [Required]
  [MinLength(10)]
  public string Summary { get; set; }

  [Required]
  public string Content { get; set; }
}