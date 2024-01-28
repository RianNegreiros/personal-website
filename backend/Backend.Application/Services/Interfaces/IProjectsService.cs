using Backend.Application.Models.InputModels;
using Backend.Core.Models;

namespace Backend.Application.Services.Interfaces;

public interface IProjectsService
{
    Task<List<Project>> GetProjects();
    Task<Project> GetProject(string id);
    Task<Project> CreateProject(ProjectInputModel project);
    Task<Project> UpdateProject(string id, UpdateProjectInputModel model);
    Task DeleteProject(string id);
}
