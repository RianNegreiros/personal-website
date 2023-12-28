using System.Xml.Linq;
using Backend.Application.Models;
using Backend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

public class RssController : BaseApiController
{
  private readonly IPostService _postService;
  private readonly IConfiguration _configuration;

  public RssController(IPostService postService, IConfiguration configuration)
  {
    _postService = postService;
    _configuration = configuration;
  }

  [HttpGet]
  public async Task<IActionResult> GetRssFeed()
  {
    List<PostViewModel> posts = await _postService.GetPosts();

    if (posts == null || !posts.Any())
    {
      return NotFound();
    }

    posts = posts.OrderByDescending(post => post.CreatedAt).ToList();

    XDocument rssFeed = new(
        new XDeclaration("1.0", "utf-8", "yes"),
        new XElement("rss",
            new XAttribute("version", "2.0"),
            new XElement("channel",
                new XElement("title", "Blog"),
                new XElement("link", _configuration["ClientUrl"]),
                new XElement("description", "Blog RSS Feed"),
                new XElement("language", "pt-BR"),
                posts.Select(post => new XElement("item",
                    new XElement("title", post.Title),
                    new XElement("link", $"{_configuration["ClientUrl"]}/posts/{post.Slug}"),
                    new XElement("description", post.Summary),
                    new XElement("pubDate", post.CreatedAt.ToString("R"))
                ))
            )
        )
    );

    return Content(rssFeed.ToString(), "application/xml");
  }
}
