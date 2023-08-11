using Backend.Core.Models;

namespace Backend.Core.Inferfaces.Repositories;
public interface IProjectsRepository
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectByIdAsync(string id);
    Task CreateProjectAsync(Project project);
    Task UpdateProjectAsync(string id, Project project);
    Task DeleteProjectAsync(string id);
}
