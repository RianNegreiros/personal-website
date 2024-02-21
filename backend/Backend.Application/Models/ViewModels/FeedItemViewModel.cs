using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Models.ViewModels
{
    public class FeedItemViewModel
    {
        [SwaggerSchema("The unique identifier of the feed item.")]
        public string Id { get; set; }

        [SwaggerSchema("The title of the feed item.")]
        public string Title { get; set; }

        [SwaggerSchema("The summary of the feed item.")]
        public string Summary { get; set; }

        [SwaggerSchema("The content of the feed item.")]
        public string Content { get; set; }

        [SwaggerSchema("The slug of the feed item.")]
        public string Slug { get; set; }

        [SwaggerSchema("The creation date of the feed item.")]
        public DateTime CreatedAt { get; set; }

        [SwaggerSchema("The last update date of the feed item.")]
        public DateTime UpdatedAt { get; set; }

        [SwaggerSchema("The URL of the feed item.")]
        public string Url { get; set; }

        [SwaggerSchema("The image URL of the feed item.")]
        public string ImageUrl { get; set; }

        [SwaggerSchema("The overview of the feed item.")]
        public string Overview { get; set; }

        [SwaggerSchema("The type of the feed item. Can be 'Post' or 'Project'.")]
        public string Type { get; set; }
    }
}