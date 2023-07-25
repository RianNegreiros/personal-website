using System.Security.Claims;
using backend.API.DTOs;
using backend.Core.Interfaces.Services;
using backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Infrastructure.Services;

namespace backend.API.Controllers;

public class PostController : BaseApiController
{
  private readonly IPostService _postService;
  private readonly IUserService _userService;
  private readonly CloudinaryService _cloudinaryService;

  public PostController(IPostService postService, IUserService userService, CloudinaryService cloudinaryService)
  {
    _postService = postService;
    _userService = userService;
    _cloudinaryService = cloudinaryService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Post>> CreatePost([FromForm] PostDto model)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    var currentUser = await _userService.GetCurrentUser(User.FindFirstValue(ClaimTypes.Email));
    if (currentUser == null)
      return Unauthorized();

    var post = await _postService.CreatePost(model, currentUser);
    return Ok(post);
  }

  [Authorize]
  [HttpPut("{id}")]
  public async Task<ActionResult<Post>> UpdatePost(string id, [FromForm] PostDto model)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    var currentUser = await _userService.GetCurrentUser(User.FindFirstValue(ClaimTypes.Email));
    if (currentUser == null)
      return Unauthorized();

    var post = await _postService.UpdatePost(id, model, currentUser);
    return Ok(post);
  }

  [HttpGet]
  public async Task<IActionResult> GetPosts()
  {
    var posts = await _postService.GetPosts();
    return Ok(posts);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetPost(string id)
  {
    var post = await _postService.GetPost(id);
    if (post == null)
      return NotFound();

    return Ok(post);
  }
}