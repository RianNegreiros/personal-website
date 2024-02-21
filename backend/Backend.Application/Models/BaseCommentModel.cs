using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class BaseCommentModel
{
    [SwaggerSchema(Description = "The comment's content")]
    public string Content { get; set; }
}