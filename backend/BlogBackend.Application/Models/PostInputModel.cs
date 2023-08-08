using System.ComponentModel.DataAnnotations;

namespace BlogBackend.Application.Models;

public class PostInputModel
{
  [Required]
  [MinLength(4)]
  public string Title { get; set; }

  [Required]
  [MinLength(10)]
  public string Summary { get; set; }

  [Required]
  public string Content { get; set; }
}