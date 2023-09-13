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
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(ApiResponse<PostViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<ApiResponse<PostViewModel>>> CreatePost([FromBody] PostInputModel model)
  {
    var validationResult = ValidateModel<PostInputModelValidator, PostInputModel>(model);

    if (!validationResult.IsValid)
      return BadRequest(new ApiResponse<PostViewModel>
      {
        Success = false,
        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
      });

      var currentUser = await _userManager.FindByIdAsync(model.AuthorId);

      var post = await _postService.CreatePost(model, currentUser);

      var postViewModel = new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Slug = post.Slug,
        Content = post.Content
      };

      return Ok(postViewModel);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update a post.")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(ApiResponse<PostViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<ApiResponse<PostViewModel>>> UpdatePost(string id, [FromForm] PostInputModel model)
  {
    var validationResult = ValidateModel<PostInputModelValidator, PostInputModel>(model);

    if (!validationResult.IsValid)
      return BadRequest(new ApiResponse<PostViewModel>
      {
        Success = false,
        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
      });

      var currentUser = await _userManager.GetUserAsync(User);

      var post = await _postService.UpdatePost(id, model, currentUser);

      var postViewModel = new PostViewModel
      {
        Id = post.Id,
        Title = post.Title,
        Summary = post.Summary,
        Slug = post.Slug,
        Content = post.Content
      };

      return Ok(postViewModel);
  }

  [AllowAnonymous]
  [HttpGet]
  [SwaggerOperation(Summary = "Get all posts.")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(List<PostViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<IEnumerable<PostViewModel>>> GetPosts()
  {
    var posts = await _postService.GetPosts();
    return Ok(posts);
  }

  [AllowAnonymous]
  [HttpGet("{identifier}")]
  [SwaggerOperation(Summary = "Get a post by ID or slug.")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(PostViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<PostViewModel>> GetPost(string identifier)
  {
    if (ObjectId.TryParse(identifier, out ObjectId objectId)) // Check if it's a valid ObjectId (ID)
    {
      var post = await _postService.GetPostById(objectId.ToString());
      if (post == null)
        return NotFound();

      return Ok(post);
    }
    else // If not a valid ObjectId, treat it as a slug
    {
      var post = await _postService.GetPostBySlug(identifier);
      if (post == null)
        return NotFound();

      return Ok(post);
    }
  }
}
