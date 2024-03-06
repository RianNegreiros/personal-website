using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

using Backend.Application.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers;

public class RssController(IPostService postService) : BaseApiController
{
    private readonly IPostService _postService = postService;

    [HttpGet]
    [ResponseCache(Duration = 86400)]
    [SwaggerOperation(Summary = "Get Posts RSS Feed")]
    public async Task<IActionResult> Rss()
    {
        var posts = await _postService.GetPosts();

        var feed = new SyndicationFeed(
            "riannegreiros.dev Blog RSS Feed",
            "RSS feed with all posts from riannegreiros.dev",
            new Uri("https://riannegreiros.dev"),
            "https://riannegreiros.dev/api/rss", DateTime.Now);

        var items = new List<SyndicationItem>();

        foreach (var post in posts)
        {
            var item = new SyndicationItem(
                post.Title,
                post.Content,
                new Uri($"https://riannegreiros.dev/posts/{post.Slug}"),
                post.Id.ToString(),
                post.UpdatedAt)
            {
                Copyright = new TextSyndicationContent($"Copyright {DateTime.Now.Year}"),
                Authors = { new SyndicationPerson("riannegreiros@gmail.com", "Rian Negreiros Dos Santos", "https://riannegreiros.dev") },
                PublishDate = post.CreatedAt,
                Summary = new TextSyndicationContent(post.Summary),
            };

            items.Add(item);
        }

        feed.Items = items;

        using var stringWriter = new StringWriterWithEncoding(Encoding.UTF8);
        using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 });
        feed.SaveAsRss20(xmlWriter);
        xmlWriter.Flush();
        return Content(stringWriter.ToString(), "application/rss+xml");
    }

    private class StringWriterWithEncoding(Encoding encoding) : StringWriter
    {
        private readonly Encoding _encoding = encoding;

        public override Encoding Encoding => _encoding;
    }
}