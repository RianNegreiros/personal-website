using Backend.API.Models;
using Backend.Application.Models;
using Backend.Application.Services;
using Backend.Application.Validators;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers;

[Authorize(Roles = "Admin")]
public class ProjectsController : BaseApiController
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(Summary = "Get all projects.")]
    [ProducesResponseType(typeof(ApiResponse<List<Project>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Project>>> GetProjects()
    {
        List<Project> projects = await _projectsService.GetProjects();
        return Ok(new ApiResponse<List<Project>>
        {
            Success = true,
            Data = projects
        });
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a project by id.")]
    [ProducesResponseType(typeof(ApiResponse<Project>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Project>> GetProject(string id)
    {
        Project project = await _projectsService.GetProject(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse<Project>
        {
            Success = true,
            Data = project
        });
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new project.")]
    [ProducesResponseType(typeof(ApiResponse<Project>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<Project>>> CreateProject([FromForm] ProjectInputModel model)
    {
        FluentValidation.Results.ValidationResult validationResult = ValidateModel<ProjectInputModelValidator, ProjectInputModel>(model);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponse<Project>
            {
                Success = false,
                Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
            });

        Project project = await _projectsService.CreateProject(model);

        return Ok(new ApiResponse<Project>
        {
            Success = true,
            Data = project
        });
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a project.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [SwaggerOperation(Summary = "Delete a project.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Project>> DeleteProject(string id)
    {
        Project project = await _projectsService.GetProject(id);

        if (project == null)
        {
            return NotFound();
        }

        await _projectsService.DeleteProject(id);

        return NoContent();
    }
}
