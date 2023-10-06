using Backend.API.Models;
using Backend.Application.Models;
using Backend.Application.Services;
using Backend.Infrastructure.Caching;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers;

public class FeedController : BaseApiController
{
  private readonly IPostService _postService;
  private readonly ICachingService _cachingService;

  public FeedController(IPostService postService, ICachingService cachingService)
  {
    _postService = postService;
    _cachingService = cachingService;
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all posts")]
  [ProducesResponseType(typeof(ApiResponse<List<PostViewModel>>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<ApiResponse<PostViewModel>>> GetPosts()
  {
    var posts = new List<PostViewModel>();
    var cacheKey = $"posts";

    var postsCache = await _cachingService.GetAsync(cacheKey);

    if (!string.IsNullOrWhiteSpace(postsCache))
    {
      posts = JsonConvert.DeserializeObject<List<PostViewModel>>(postsCache);

      if (posts == null)
        return NotFound();

      return Ok(new ApiResponse<List<PostViewModel>>
      {
        Success = true,
        Data = posts
      });
    }

    posts = await _postService.GetPosts();

    if (posts == null)
      return NotFound();

    await _cachingService.SetAsync(cacheKey, JsonConvert.SerializeObject(posts));

    return Ok(new ApiResponse<List<PostViewModel>>
    {
      Success = true,
      Data = posts
    });
  }
}
