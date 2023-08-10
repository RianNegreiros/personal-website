using BlogBackend.Core.Inferfaces;
using BlogBackend.Core.Models;

namespace BlogBackend.Application.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        public async Task<List<Project>> GetProjects()
        {
            return await _projectsRepository.GetAllProjectsAsync();
        }

        public async Task<Project> GetProject(string id)
        {
            return await _projectsRepository.GetProjectByIdAsync(id);
        }

        public async Task<Project> CreateProject(Project project)
        {
            var newProject = new Project
            {
                Title = project.Title,
                Overview = project.Overview,
                ImageUrl = project.ImageUrl,
                Url = project.Url,
            };
            await _projectsRepository.CreateProjectAsync(newProject);
            return project;
        }

        public async Task<Project> UpdateProject(string id, Project project)
        {
            await _projectsRepository.UpdateProjectAsync(id, project);
            return project;
        }

        public async Task DeleteProject(string id)
        {
            await _projectsRepository.DeleteProjectAsync(id);
        }
    }
}