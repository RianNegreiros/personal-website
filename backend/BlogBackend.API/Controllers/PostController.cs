using System.Security.Claims;
using BlogBackend.API.Models;
using BlogBackend.Application.Models;
using BlogBackend.Application.Services;
using BlogBackend.Application.Validators;
using BlogBackend.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackend.API.Controllers;

public class PostController : BaseApiController
{
  private readonly IPostService _postService;
  private readonly IUserService _userService;

  public PostController(IPostService postService, IUserService userService)
  {
    _postService = postService;
    _userService = userService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<ApiResponse<PostViewModel>>> CreatePost([FromForm] PostInputModel model)
  {
    var validator = new PostInputModelValidator();
    var validationResult = await validator.ValidateAsync(model);

    if (!validationResult.IsValid)
    {
      return BadRequest(new ApiResponse<PostViewModel>
      {
        Success = false,
        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
      });
    }

    try
    {
      var currentUser = await _userService.GetCurrentUser(User.FindFirstValue(ClaimTypes.Email));
      if (currentUser == null)
        return Unauthorized();

      var post = await _postService.CreatePost(model, currentUser);

      var postViewModel = new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Content = post.Content,
        CoverImageUrl = post.CoverImageUrl
      };

      return Ok(postViewModel);
    }
    catch (PostNotFoundException ex)
    {
      return NotFound(ex.Message);
    }
    catch (AuthorizationException ex)
    {
      return Unauthorized(ex.Message);
    }
    catch (ImageUploadException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception)
    {
      return BadRequest("An error occurred while processing the request.");
    }
  }

  [Authorize]
  [HttpPut("{id}")]
  public async Task<ActionResult<PostViewModel>> UpdatePost(string id, [FromForm] PostInputModel model)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    try
    {
      var currentUser = await _userService.GetCurrentUser(User.FindFirstValue(ClaimTypes.Email));
      if (currentUser == null)
        return Unauthorized();

      var post = await _postService.UpdatePost(id, model, currentUser);

      var postViewModel = new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Content = post.Content,
        CoverImageUrl = post.CoverImageUrl
      };

      return Ok(postViewModel);
    }
    catch (PostNotFoundException ex)
    {
      return NotFound(ex.Message);
    }
    catch (AuthorizationException ex)
    {
      return Unauthorized(ex.Message);
    }
    catch (ImageUploadException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception)
    {
      return BadRequest("An error occurred while processing the request.");
    }
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