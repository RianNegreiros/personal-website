using Backend.Application.Services.Interfaces;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/[controller]")]
public class ProjectsController : ControllerBase
{
  private readonly IProjectsRepository _projectsRepository;

  public ProjectsController(IProjectsRepository projectsRepository)
  {
    _projectsRepository = projectsRepository;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Project>>> GetAll()
  {
    var projects = await _projectsRepository.GetAllProjectsAsync();
    return Ok(projects);
  }

  [HttpGet("{id}")]
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
  public async Task<IActionResult> Delete(string id)
  {
    await _projectsRepository.DeleteProjectAsync(id);
    return NoContent();
  }
}