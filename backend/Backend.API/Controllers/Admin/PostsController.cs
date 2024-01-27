using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/[controller]")]
public class PostsController : ControllerBase
{
  private readonly IPostRepository _postRepository;

  public PostsController(IPostRepository postRepository)
  {
    _postRepository = postRepository;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Post>>> GetAll()
  {
    var posts = await _postRepository.GetAll();
    return Ok(posts);
  }

  [HttpGet("{id}")]
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
  public async Task<IActionResult> Delete(string id)
  {
    await _postRepository.Delete(id);
    return NoContent();
  }
}