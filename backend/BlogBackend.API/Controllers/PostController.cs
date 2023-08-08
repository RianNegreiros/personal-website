using System.Security.Claims;
using BlogBackend.API.Models;
using BlogBackend.Application.Models;
using BlogBackend.Application.Services;
using BlogBackend.Application.Validators;
using BlogBackend.Core.Exceptions;
using BlogBackend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackend.API.Controllers;

public class PostController : BaseApiController
{
  private readonly IPostService _postService;
  private readonly IUserService _userService;
  private readonly UserManager<User> _userManager;

  public PostController(IPostService postService, IUserService userService, UserManager<User> userManager)
  {
    _postService = postService;
    _userService = userService;
    _userManager = userManager;
  }

  [Authorize]
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

  [Authorize]
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

  [Authorize]
  [HttpPost("upload")]
  public async Task<ActionResult<ApiResponse<string>>> UploadImage([FromForm] IFormFile image)
  {
    try
    {
      var imageUrl = await _postService.UploadImageAsync(image);
      return Ok(imageUrl);
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

  private FluentValidation.Results.ValidationResult ValidateModel(PostInputModel model)
  {
    var validator = new PostInputModelValidator();
    return validator.Validate(model);
  }
}
