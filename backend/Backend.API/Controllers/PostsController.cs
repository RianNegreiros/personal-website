using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Backend.API.Models;
using Backend.Application.Models;
using Backend.Application.Models.InputModels;
using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Application.Validators;
using Backend.Core.Models;
using Backend.Infrastructure.Caching;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers;

public class PostsController : BaseApiController
{
  private readonly IPostService _postService;
  private readonly UserManager<User> _userManager;
  private readonly ICachingService _cachingService;

  public PostsController(IPostService postService, UserManager<User> userManager, ICachingService cachingService)
  {
    _postService = postService;
    _userManager = userManager;
    _cachingService = cachingService;
  }

  [ResponseCache(Duration = 1200)]
  [HttpGet("rss")]
  [SwaggerOperation(Summary = "Get Posts RSS Feed")]
  public async Task<IActionResult> Rss()
  {
    List<PostViewModel> posts = await _postService.GetPosts();

    var feed = new SyndicationFeed(
        "Rian Negreiros Blog Feed",
        "RSS Feed from Rian Negreiros Dos Santos Blog",
        new Uri("https://www.riannegreiros.dev"),
        posts.Select(post =>
        {
          var item = new SyndicationItem(
              title: post.Title,
              content: post.Content,
              new Uri($"https://www.riannegreiros.dev/posts/{post.Slug}"),
              id: post.Id.ToString(),
              new DateTimeOffset(post.CreatedAt)
          )
          {
            PublishDate = post.CreatedAt,
            Copyright = new TextSyndicationContent($"Copyright {post.CreatedAt.Year}"),
            Summary = new TextSyndicationContent(post.Summary),
            LastUpdatedTime = post.UpdatedAt
          };
          return item;
        }).ToList()
    );

    var settings = new XmlWriterSettings
    {
      Encoding = Encoding.UTF8,
      NewLineHandling = NewLineHandling.Entitize,
      NewLineOnAttributes = true,
      Indent = true
    };

    using var stream = new MemoryStream();
    using (var xmlWriter = XmlWriter.Create(stream, settings))
    {
      var rssFormatter = new Rss20FeedFormatter(feed, false);
      rssFormatter.WriteTo(xmlWriter);
      xmlWriter.Flush();
    }
    return File(stream.ToArray(), "application/rss+xml; charset=utf-8");
  }

  [Authorize(Roles = "Admin")]
  [HttpPost]
  [SwaggerOperation(Summary = "Create a new post.")]
  [ProducesResponseType(typeof(ApiResponse<PostViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<ApiResponse<PostViewModel>>> CreatePost([FromBody] PostInputModel model)
  {
    FluentValidation.Results.ValidationResult validationResult = ValidateModel<PostInputModelValidator, PostInputModel>(model);

    if (!validationResult.IsValid)
      return BadRequest(new ApiResponse<PostViewModel>
      {
        Success = false,
        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
      });

    User currentUser = await _userManager.FindByIdAsync(model.AuthorId);

    Post post = await _postService.CreatePost(model, currentUser);

    return Ok(new ApiResponse<PostViewModel>
    {
      Success = true,
      Data = new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Slug = post.Slug,
        Content = post.Content
      }
    });
  }

  [Authorize(Roles = "Admin")]
  [HttpPut("{identifier}")]
  [SwaggerOperation(Summary = "Update a post by id or slug.")]
  [ProducesResponseType(typeof(ApiResponse<PostViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<ApiResponse<PostViewModel>>> UpdatePost(string identifier, [FromForm] UpdatePostModel model)
  {
    FluentValidation.Results.ValidationResult validationResult = ValidateModel<UpdatePostModelValidator, UpdatePostModel>(model);

    if (!validationResult.IsValid)
      return BadRequest(new ApiResponse<PostViewModel>
      {
        Success = false,
        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
      });

    User currentUser = await _userManager.FindByIdAsync(model.AuthorId);

    if (currentUser == null)
      return BadRequest(new ApiResponse<PostViewModel>
      {
        Success = false,
        Errors = new List<string> { "User not found" }
      });

    Post post = await _postService.UpdatePost(identifier, model, currentUser);

    return Ok(new ApiResponse<PostViewModel>
    {
      Success = true,
      Data = new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Slug = post.Slug,
        Content = post.Content
      }
    });
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get posts paginated.")]
  [ProducesResponseType(typeof(ApiResponse<List<PostViewModel>>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<ApiResponse<PaginatedResult<PostViewModel>>>> GetPostsPaginated([FromQuery] QueryParameters parameters)
  {
    var posts = new PaginatedResult<PostViewModel>();
    var cacheKey = $"posts-{parameters.PageNumber}-{parameters.PageSize}";

    var postsCache = await _cachingService.GetAsync(cacheKey);

    if (!string.IsNullOrWhiteSpace(postsCache))
    {
      posts = JsonConvert.DeserializeObject<PaginatedResult<PostViewModel>>(postsCache);

      if (posts == null)
        return NotFound();

      return Ok(new ApiResponse<PaginatedResult<PostViewModel>>
      {
        Success = true,
        Data = posts
      });
    }

    posts = await _postService.GetPaginatedPosts(parameters.PageNumber, parameters.PageSize);

    if (posts == null)
      return NotFound();

    await _cachingService.SetAsync(cacheKey, JsonConvert.SerializeObject(posts));

    return Ok(new ApiResponse<PaginatedResult<PostViewModel>>
    {
      Success = true,
      Data = posts
    });
  }

  [HttpGet("{identifier}")]
  [SwaggerOperation(Summary = "Get a post by ID or slug.")]
  [ProducesResponseType(typeof(ApiResponse<PostViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<PostViewModel>> GetPost(string identifier)
  {
    PostViewModel? post;
    var postCache = await _cachingService.GetAsync(identifier);

    if (!string.IsNullOrWhiteSpace(postCache))
    {
      post = JsonConvert.DeserializeObject<PostViewModel>(postCache);

      if (post == null)
        return NotFound();

      return Ok(new ApiResponse<PostViewModel>
      {
        Success = true,
        Data = post
      });
    }

    post = await _postService.GetPostByIdentifier(identifier);

    if (post == null)
      return NotFound();

    await _cachingService.SetAsync(identifier, JsonConvert.SerializeObject(post));

    return Ok(new ApiResponse<PostViewModel>
    {
      Success = true,
      Data = post
    });
  }
}
