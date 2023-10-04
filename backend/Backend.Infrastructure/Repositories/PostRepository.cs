using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IMongoCollection<Post> _postCollection;

    public PostRepository(IMongoDatabase database)
    {
        _postCollection = database.GetCollection<Post>("posts");
    }

    public async Task<Post> Create(Post post)
    {
        await _postCollection.InsertOneAsync(post);
        return post;
    }

    public async Task<Post> Update(Post post)
    {
        await _postCollection.ReplaceOneAsync(p => p.Id == post.Id, post);
        return post;
    }

    public async Task<Post> GetById(string id)
    {
        return await _postCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Post> GetBySlug(string slug)
    {
        return await _postCollection.Find(p => p.Slug == slug).FirstOrDefaultAsync();
    }

    public async Task<List<Post>> GetAllWithParameters(int pageNumber, int pageSize)
    {
        return await _postCollection.Find(_ => true)
            .SortByDescending(p => p.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task<List<Post>> GetAll()
    {
        return await _postCollection.Find(_ => true).ToListAsync();
    }

    public async Task<int> Count()
    {
        return (int) await _postCollection.CountDocumentsAsync(_ => true);
    }
}