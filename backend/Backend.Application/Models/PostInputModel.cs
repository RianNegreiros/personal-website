using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class PostInputModel : BasePostModel
{
  [SwaggerSchema(Description = "The author's id", Nullable = true)]
  public string? AuthorId { get; set; }
}