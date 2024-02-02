using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/[controller]")]
[SwaggerTag("To admins manage posts in the system.")]
public class PostsController : ControllerBase
{
  private readonly IPostRepository _postRepository;

  public PostsController(IPostRepository postRepository)
  {
    _postRepository = postRepository;
  }

  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<Post>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<IEnumerable<Post>>> GetAll()
  {
    var posts = await _postRepository.GetAll();
    return Ok(posts);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<Post>> GetById(string id)
  {
    var post = await _postRepository.GetById(id);

    if (post == null)
    {
      return NotFound();
    }

    return Ok(post);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Delete(string id)
  {
    await _postRepository.Delete(id);
    return NoContent();
  }
}