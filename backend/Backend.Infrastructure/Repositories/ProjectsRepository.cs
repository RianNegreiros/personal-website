using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly IMongoCollection<Project> _projectsCollection;

        public ProjectsRepository(IMongoDatabase database)
        {
            _projectsCollection = database.GetCollection<Project>("projects");
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _projectsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(string id)
        {
            return await _projectsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateProjectAsync(Project project)
        {
            await _projectsCollection.InsertOneAsync(project);
        }

        public async Task UpdateProjectAsync(string id, Project project)
        {
            await _projectsCollection.ReplaceOneAsync(p => p.Id == id, project);
        }

        public async Task DeleteProjectAsync(string id)
        {
            await _projectsCollection.DeleteOneAsync(p => p.Id == id);
        }
    }
}