using Backend.Core.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class CommentViewModel : BaseCommentModel
{
    [SwaggerSchema(Description = "The comment's id")]
    public string Id { get; set; }
    
    [SwaggerSchema(Description = "The comment's author")]
    public User Author { get; set; }

    [SwaggerSchema(Description = "The comment's creation date")]
    public DateTime CreatedAt { get; set; }
}
