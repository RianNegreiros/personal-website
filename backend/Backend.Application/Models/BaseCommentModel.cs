using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class BaseCommentModel
{
    [SwaggerSchema(Description = "The comment's id")]
    public string Content { get; set; }
}
