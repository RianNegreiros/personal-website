using Backend.API.Models;
using Backend.Application.Services.Interfaces;
using Backend.Infrastructure.Caching;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers;

public class FeedController : BaseApiController
{
  private readonly IFeedService _feedService;
  private readonly ICachingService _cachingService;

  public FeedController(IFeedService feedService, ICachingService cachingService)
  {
    _feedService = feedService;
    _cachingService = cachingService;
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get feed with posts and projects")]
  [ProducesResponseType(typeof(ApiResponse<List<object>>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<ApiResponse<object>>> GetPosts()
  {
    _ = new List<object>();
    var cacheKey = $"feed";

    var feedCache = await _cachingService.GetAsync(cacheKey);
    List<object>? feed;
    if (!string.IsNullOrWhiteSpace(feedCache))
    {
      feed = JsonConvert.DeserializeObject<List<object>>(feedCache);

      if (feed == null)
        return NotFound();

      return Ok(new ApiResponse<List<object>>
      {
        Success = true,
        Data = feed
      });
    }

    feed = await _feedService.GetFeed();

    if (feed == null)
      return NotFound();

    await _cachingService.SetAsync(cacheKey, JsonConvert.SerializeObject(feed));

    return Ok(new ApiResponse<List<object>>
    {
      Success = true,
      Data = feed
    });
  }
}
