using Backend.API.Models;
using Backend.Application.Models;
using Backend.Application.Services;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class ProjectsController : BaseApiController
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            return Ok(await _projectsService.GetProjects());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(string id)
        {
            var project = await _projectsService.GetProject(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Project>>> CreateProject([FromForm] ProjectInputModel model)
        {

            var project = await _projectsService.CreateProject(model);

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> UpdateProject(string id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            await _projectsService.UpdateProject(id, project);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(string id)
        {
            var project = await _projectsService.GetProject(id);

            if (project == null)
            {
                return NotFound();
            }

            await _projectsService.DeleteProject(id);

            return NoContent();
        }
    }
}