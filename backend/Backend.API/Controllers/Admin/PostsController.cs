using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers.Admin;

[SwaggerTag("To admins manage posts in the system.")]
public class PostsController : AdminBaseApiController
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

        return post == null ? (ActionResult<Post>)NotFound() : (ActionResult<Post>)Ok(post);
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