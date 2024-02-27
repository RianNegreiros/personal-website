using Backend.Application.Models.InputModels;
using Backend.Application.Services.Interfaces;
using Backend.Core.Interfaces.CloudServices;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

namespace Backend.Application.Services.Implementations;

public class ProjectsService(IProjectsRepository projectsRepository, ICloudinaryService cloudinaryService) : IProjectsService
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;
    private readonly ICloudinaryService _cloudinaryService = cloudinaryService;

    public async Task<List<ProjectViewModel>> GetProjects()
    {
        List<Project> projects = await _projectsRepository.GetAllProjectsAsync();
        return projects != null
            ? projects.Select(project => new ProjectViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Overview = project.Overview,
                Url = project.Url,
                ImageUrl = project.ImageUrl,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            }).ToList()
            : (List<ProjectViewModel>)([]);
    }

    public async Task<ProjectViewModel> GetProject(string id)
    {
        Project project = await _projectsRepository.GetProjectByIdAsync(id);

        return new ProjectViewModel
        {
            Id = project.Id,
            Title = project.Title,
            Overview = project.Overview,
            Url = project.Url,
            ImageUrl = project.ImageUrl,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }

    public async Task<Project> CreateProject(ProjectInputModel model, User author)
    {
        string imageUrl = await _cloudinaryService.UploadImageAsync(model.Image.OpenReadStream(), model.Image.FileName);

        Project newProject = new()
        {
            Title = model.Title,
            Overview = model.Overview,
            ImageUrl = imageUrl,
            Url = model.Url,
            Author = author,
            CreatedAt = DateTime.UtcNow
        };

        await _projectsRepository.CreateProjectAsync(newProject);
        return newProject;
    }

    public async Task<Project> UpdateProject(string id, UpdateProjectInputModel model)
    {
        var project = await _projectsRepository.GetProjectByIdAsync(id);

        string imageUrl = project.Url;

        if (model.Image != null)
        {
            imageUrl = await _cloudinaryService.UploadImageAsync(model.Image.OpenReadStream(), model.Image.FileName);
        }

        if (model.Title != null)
        {
            project.Title = model.Title;
        }

        if (model.Url != null)
        {
            project.Url = model.Url;
        }

        if (model.Overview != null)
        {
            project.Overview = model.Overview;
        }

        project.Url = imageUrl;
        project.UpdatedAt = DateTime.Now;

        await _projectsRepository.UpdateProjectAsync(id, project);

        return project;
    }

    public async Task DeleteProject(string id)
    {
        await _projectsRepository.DeleteProjectAsync(id);
    }
}