using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers.Admin;

[SwaggerTag("To admins manage projects in the system.")]
public class ProjectsController : AdminBaseApiController
{
  private readonly IProjectsRepository _projectsRepository;

  public ProjectsController(IProjectsRepository projectsRepository)
  {
    _projectsRepository = projectsRepository;
  }

  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<Project>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<IEnumerable<Project>>> GetAll()
  {
    var projects = await _projectsRepository.GetAllProjectsAsync();
    return Ok(projects);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(Project), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<Project>> GetById(string id)
  {
    var project = await _projectsRepository.GetProjectByIdAsync(id);

    if (project == null)
    {
      return NotFound();
    }

    return Ok(project);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Delete(string id)
  {
    await _projectsRepository.DeleteProjectAsync(id);
    return NoContent();
  }
}