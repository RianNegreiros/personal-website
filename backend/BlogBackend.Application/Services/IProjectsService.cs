using BlogBackend.Core.Models;

namespace BlogBackend.Application.Services
{
    public interface IProjectsService
    {
        Task<List<Project>> GetProjects();
        Task<Project> GetProject(string id);
        Task<Project> CreateProject(Project project);
        Task<Project> UpdateProject(string id, Project project);
        Task DeleteProject(string id);
    }
}