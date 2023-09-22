using Backend.API.Models;
using Backend.Application.Models;
using Backend.Application.Services;
using Backend.Application.Validators;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers;

[Authorize(Roles = "Admin")]
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

  [HttpPut("{identifier}")]
  [SwaggerOperation(Summary = "Update a post.")]
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

  [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
  [AllowAnonymous]
  [HttpGet]
  [SwaggerOperation(Summary = "Get all posts.")]
  [ProducesResponseType(typeof(ApiResponse<List<PostViewModel>>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPosts()
  {
    List<PostViewModel> posts = await _postService.GetPosts();
    return Ok(new ApiResponse<List<PostViewModel>>
    {
      Success = true,
      Data = posts
    });
  }

  [AllowAnonymous]
  [HttpGet("{identifier}")]
  [SwaggerOperation(Summary = "Get a post by ID or slug.")]
  [ProducesResponseType(typeof(ApiResponse<PostViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<PostViewModel>> GetPost(string identifier)
  {
    PostViewModel? post;
    if (ObjectId.TryParse(identifier, out ObjectId objectId)) // Check if it's a valid ObjectId (ID)
    {
      post = await _postService.GetPostById(objectId.ToString());
      if (post == null)
        return NotFound();

      return Ok(post);
    }

    // If not a valid ObjectId, treat it as a slug
    post = await _postService.GetPostBySlug(identifier);
    if (post == null)
      return NotFound();

    return Ok(new ApiResponse<PostViewModel>
    {
      Success = true,
      Data = post
    });
  }
}
