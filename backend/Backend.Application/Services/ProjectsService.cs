using Backend.Application.Models;
using Backend.Core.CloudServices;
using Backend.Core.Inferfaces;
using Backend.Core.Models;

namespace Backend.Application.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public ProjectsService(IProjectsRepository projectsRepository, ICloudinaryService cloudinaryService)
        {
            _projectsRepository = projectsRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<List<Project>> GetProjects()
        {
            return await _projectsRepository.GetAllProjectsAsync();
        }

        public async Task<Project> GetProject(string id)
        {
            return await _projectsRepository.GetProjectByIdAsync(id);
        }

        public async Task<Project> CreateProject(ProjectInputModel model)
        {
            var imageUrl = await _cloudinaryService.UploadImageAsync(model.Image.OpenReadStream(), model.Image.FileName);

            var newProject = new Project
            {
                Title = model.Title,
                Overview = model.Overview,
                ImageUrl = imageUrl,
                Url = model.Url
            };

            await _projectsRepository.CreateProjectAsync(newProject);
            return newProject;
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