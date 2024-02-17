using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models.InputModels;

public class CommentInputModel : BaseCommentModel
{
    [SwaggerSchema(Description = "The user's id")]
    public string AuthorId { get; set; }
}
