using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers.Admin;

[SwaggerTag("To admins manage comments in the system.")]
public class CommentsController : AdminBaseApiController
{
    private readonly ICommentsRepository _commentsRepository;

    public CommentsController(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Comment>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAll()
    {
        var comments = await _commentsRepository.GetAll();
        return Ok(comments);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Comment), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        await _commentsRepository.Delete(id);
        return NoContent();
    }
}