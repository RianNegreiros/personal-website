using BlogBackend.API.Models;
using BlogBackend.Application.Models;
using BlogBackend.Application.Services;
using BlogBackend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackend.API.Controllers
{
    public class ProjectsController : BaseApiController
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            return Ok(await _projectsService.GetProjects());
        }

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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Project>>> CreateProject([FromForm] ProjectInputModel model)
        {

            var project = await _projectsService.CreateProject(model);

            return Ok(project);
        }

        [Authorize]
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

        [Authorize]
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