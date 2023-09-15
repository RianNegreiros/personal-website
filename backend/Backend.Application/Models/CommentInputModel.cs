using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class CommentInputModel : BaseCommentModel
{
    [SwaggerSchema(Description = "The user's token")]
    public string token { get; set; }
}
