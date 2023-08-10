using Backend.API.Models;
using Backend.Application.Models;
using Backend.Application.Services;
using Backend.Application.Validators;
using Backend.Core.Exceptions;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[Authorize(Policy = "AdminPolicy")]
public class PostController : BaseApiController
{
  private readonly IPostService _postService;
  private readonly UserManager<User> _userManager;

  public PostController(IPostService postService, UserManager<User> userManager)
  {
    _postService = postService;
    _userManager = userManager;
  }

  [HttpPost]
  public async Task<ActionResult<ApiResponse<PostViewModel>>> CreatePost([FromBody] PostInputModel model)
  {
    var validationResult = ValidateModel(model);

    if (!validationResult.IsValid)
      return BadRequest(new ApiResponse<PostViewModel>
      {
        Success = false,
        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
      });

    try
    {

      var currentUser = await _userManager.GetUserAsync(User);

      var post = await _postService.CreatePost(model, currentUser);

      var postViewModel = new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Content = post.Content
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

  [HttpPut("{id}")]
  public async Task<ActionResult<ApiResponse<PostViewModel>>> UpdatePost(string id, [FromForm] PostInputModel model)
  {
    var validationResult = ValidateModel(model);

    if (!validationResult.IsValid)
      return BadRequest(new ApiResponse<PostViewModel>
      {
        Success = false,
        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
      });

    try
    {
      var currentUser = await _userManager.GetUserAsync(User);

      var post = await _postService.UpdatePost(id, model, currentUser);

      var postViewModel = new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Content = post.Content
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
  public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPosts()
  {
    var posts = await _postService.GetPosts();
    return Ok(posts);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<PostViewModel>> GetPost(string id)
  {
    var post = await _postService.GetPost(id);
    if (post == null)
      return NotFound();

    return Ok(post);
  }

  private FluentValidation.Results.ValidationResult ValidateModel(PostInputModel model)
  {
    var validator = new PostInputModelValidator();
    return validator.Validate(model);
  }
}
