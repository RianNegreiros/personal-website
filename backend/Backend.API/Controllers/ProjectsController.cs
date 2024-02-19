using Backend.API.Models;
using Backend.Application.Models.InputModels;
using Backend.Application.Services.Interfaces;
using Backend.Application.Validators;
using Backend.Core.Interfaces.CloudServices;
using Backend.Core.Models;
using Backend.Infrastructure.Caching;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers;

[Authorize(Roles = "Admin")]
public class ProjectsController : BaseApiController
{
    private readonly IProjectsService _projectsService;
    private readonly UserManager<User> _userManager;
    private readonly ICachingService _cachingService;
    private readonly IEmailService _emailService;
    private readonly IBackgroundJobClient _jobClient;

    public ProjectsController(IProjectsService projectsService, UserManager<User> userManager, ICachingService cachingService, IEmailService emailService, IBackgroundJobClient jobClient)
    {
        _projectsService = projectsService;
        _userManager = userManager;
        _cachingService = cachingService;
        _emailService = emailService;
        _jobClient = jobClient;
    }

    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(Summary = "Get all projects.")]
    [ProducesResponseType(typeof(ApiResponse<List<ProjectViewModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProjectViewModel>>> GetProjects()
    {
        List<ProjectViewModel>? projects;
        string cachedProjects = await _cachingService.GetAsync("projects");
        if (!string.IsNullOrWhiteSpace(cachedProjects))
        {
            projects = JsonConvert.DeserializeObject<List<ProjectViewModel>>(cachedProjects);

            if (projects == null)
                return NotFound();

            return Ok(new ApiResponse<List<ProjectViewModel>>
            {
                Success = true,
                Data = projects
            });
        }

        projects = await _projectsService.GetProjects();

        if (projects == null)
            return NotFound();

        await _cachingService.SetAsync("projects", JsonConvert.SerializeObject(projects));

        return Ok(new ApiResponse<List<ProjectViewModel>>
        {
            Success = true,
            Data = projects
        });
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a project by id.")]
    [ProducesResponseType(typeof(ApiResponse<ProjectViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Project>> GetProject(string id)
    {
        ProjectViewModel? project;
        string cachedProject = await _cachingService.GetAsync(id);

        if (!string.IsNullOrWhiteSpace(cachedProject))
        {
            project = JsonConvert.DeserializeObject<ProjectViewModel>(cachedProject);

            if (project == null)
                return NotFound();

            return Ok(new ApiResponse<ProjectViewModel>
            {
                Success = true,
                Data = project
            });
        }

        project = await _projectsService.GetProject(id);

        if (project == null)
            return NotFound();

        await _cachingService.SetAsync(id, JsonConvert.SerializeObject(project));

        return Ok(new ApiResponse<ProjectViewModel>
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

        User currentUser = await _userManager.FindByIdAsync(model.AuthorId);

        Project project = await _projectsService.CreateProject(model, currentUser);

        string emailSubject = "Project Confirmation";
        string emailMessage = _emailService.GenerateProjectConfirmationTemplate(project.Title);
        _jobClient.Enqueue(() => _emailService.SendEmailAsync(currentUser.Email, emailSubject, emailMessage));

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
    public async Task<ActionResult<Project>> UpdateProject(string id, [FromForm] UpdateProjectInputModel model)
    {
        var project = await _projectsService.UpdateProject(id, model);

        return Ok(project);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a project.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
