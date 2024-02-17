using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models.ViewModels;

public class CommentViewModel : BaseCommentModel
{
    [SwaggerSchema(Description = "The comment's id")]
    public string Id { get; set; }

    [SwaggerSchema(Description = "The comment's post id")]
    public string? PostId { get; set; }

    [SwaggerSchema(Description = "The comment's post slug")]
    public string? PostSlug { get; set; }

    [SwaggerSchema(Description = "The comment's author id")]
    public string AuthorId { get; set; }

    [SwaggerSchema(Description = "The comment's author email")]
    public string AuthorEmail { get; set; }

    [SwaggerSchema(Description = "The comment's author username")]
    public string AuthorUsername { get; set; }

    [SwaggerSchema(Description = "The comment's replies")]
    public List<CommentViewModel>? Replies { get; set; }

    [SwaggerSchema(Description = "The comment's creation date")]
    public DateTime CreatedAt { get; set; }

    [SwaggerSchema(Description = "The comment's last change date")]
    public DateTime UpdatedAt { get; set; }
}
