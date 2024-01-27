using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/[controller]")]
public class CommentsController : ControllerBase
{
  private readonly ICommentsRepository _commentsRepository;

  public CommentsController(ICommentsRepository commentsRepository)
  {
    _commentsRepository = commentsRepository;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Comment>>> GetAll()
  {
    var comments = await _commentsRepository.GetAll();
    return Ok(comments);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Comment>> GetById(string id)
  {
    var comment = await _commentsRepository.GetById(id);

    if (comment == null)
    {
      return NotFound();
    }

    return Ok(comment);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(string id)
  {
    await _commentsRepository.Delete(id);
    return NoContent();
  }
}