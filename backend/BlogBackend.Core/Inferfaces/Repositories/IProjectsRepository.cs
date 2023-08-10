using BlogBackend.Core.Models;

namespace BlogBackend.Core.Inferfaces.Repositories;

public interface IProjectsRepository
{
    Task<List<Project>> GetProjectsAsync();
    Task<Project> GetProjectAsync(string id);
    Task<Project> CreateProjectAsync(Project project);
    Task UpdateProjectAsync(string id, Project project);
    Task DeleteProjectAsync(string id);
}
