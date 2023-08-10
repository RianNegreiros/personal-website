using BlogBackend.Core.Models;

namespace BlogBackend.Core.Inferfaces;
public interface IProjectsRepository
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<Project> GetProjectByIdAsync(string id);
    Task CreateProjectAsync(Project project);
    Task UpdateProjectAsync(string id, Project project);
    Task DeleteProjectAsync(string id);
}
