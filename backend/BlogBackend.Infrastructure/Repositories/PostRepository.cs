using BlogBackend.Core.Inferfaces.Repositories;
using BlogBackend.Core.Models;
using MongoDB.Driver;

namespace BlogBackend.Infrastructure.Repositories;

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

    public async Task<List<Post>> GetAll()
    {
        return await _postCollection.Find(_ => true).ToListAsync();
    }
}