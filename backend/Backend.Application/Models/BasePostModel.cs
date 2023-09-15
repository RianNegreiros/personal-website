using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models;

public class BasePostModel
{
    [SwaggerSchema(Description = "The post's title")]
    public string Title { get; set; }

    [SwaggerSchema(Description = "The post's summary")]
    public string Summary { get; set; }

    [SwaggerSchema(Description = "The post's content")]
    public string Content { get; set; }
}
