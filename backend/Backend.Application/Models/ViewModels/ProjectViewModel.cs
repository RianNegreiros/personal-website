using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Core.Models;

public class ProjectViewModel
{
    [SwaggerSchema(Description = "The project's id")]
    public string Id { get; set; }

    [SwaggerSchema(Description = "The project's title")]
    public string Title { get; set; }

    [SwaggerSchema(Description = "The project's repository url")]
    public string Url { get; set; }

    [SwaggerSchema(Description = "The project's overview")]
    public string Overview { get; set; }

    [SwaggerSchema(Description = "The project's image url from the cloud")]
    public string ImageUrl { get; set; }

    [SwaggerSchema(Description = "The project's date of creation")]
    public DateTime CreatedAt { get; set; }

    [SwaggerSchema(Description = "The project's last update")]
    public DateTime UpdatedAt { get; set; }
}