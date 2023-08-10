using BlogBackend.Core.Inferfaces.Repositories;
using BlogBackend.Core.Models;
using MongoDB.Driver;

namespace BlogBackend.Infrastructure.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    private readonly IMongoCollection<Project> _projectsCollection;

    public ProjectsRepository(IMongoDatabase database)
    {
        _projectsCollection = database.GetCollection<Project>("projects");
    }

    public async Task<List<Project>> GetProjectsAsync()
    {
        return await _projectsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Project> GetProjectAsync(string id)
    {
        return await _projectsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        await _projectsCollection.InsertOneAsync(project);
        return project;
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