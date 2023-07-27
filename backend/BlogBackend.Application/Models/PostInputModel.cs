using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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
  [MinLength(50)]
  public string Content { get; set; }

  public IFormFile? CoverImage { get; set; }
}