using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models.ViewModels;

public class PostViewModel : BasePostModel
{
  [SwaggerSchema(Description = "The post's id")]
  public string Id { get; set; }

  [SwaggerSchema(Description = "The post's slug")]
  public string Slug { get; set; }

  [SwaggerSchema(Description = "The post's date of creation")]
  public DateTime CreatedAt { get; set; }

  [SwaggerSchema(Description = "The post's last update")]
  public DateTime UpdatedAt { get; set; }
}