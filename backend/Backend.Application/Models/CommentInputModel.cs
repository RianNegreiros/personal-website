using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class CommentInputModel : BaseCommentModel
{
    [SwaggerSchema(Description = "The user's id")]
    public string Id { get; set; }
}
