using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class UpdatePostModel
{
    [SwaggerSchema(Description = "The post's author id")]
    public string AuthorId { get; set; }

    [SwaggerSchema(Description = "The post's title")]
    public string? Title { get; set; }

    [SwaggerSchema(Description = "The post's summary")]
    public string? Summary { get; set; }

    [SwaggerSchema(Description = "The post's content")]
    public string? Content { get; set; }
}
