using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class PostRepository(IMongoDatabase database) : IPostRepository
{
    private readonly IMongoCollection<Post> _postCollection = database.GetCollection<Post>("posts");

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
        var filter = Builders<Post>.Filter.Empty;
        var sort = Builders<Post>.Sort.Descending(p => p.CreatedAt);

        var posts = await _postCollection.FindAsync(filter, new FindOptions<Post> { Sort = sort });

        return await posts.ToListAsync();
    }

    public async Task<List<Post>> GetPostsForFeed()
    {
        var projection = Builders<Post>.Projection
            .Include(p => p.Id)
            .Include(p => p.Title)
            .Include(p => p.Summary)
            .Include(p => p.Slug)
            .Include(p => p.CreatedAt);

        return await _postCollection.Find(_ => true).Project<Post>(projection)
        .SortByDescending(p => p.CreatedAt)
        .ToListAsync();
    }

    public async Task<int> Count()
    {
        return (int)await _postCollection.CountDocumentsAsync(_ => true);
    }

    public async Task Delete(string id)
    {
        var filter = Builders<Post>.Filter.Eq("Id", id);

        await _postCollection.DeleteOneAsync(filter);
    }

    public async Task<List<Post>> GetPostsForUserById(string userId)
    {
        return await _postCollection.Find(c => c.Author.Id == userId).ToListAsync();
    }

    public async Task<List<Post>> GetPostsSuggestions(int count, string? excludeSlug = null)
    {
        var pipeline = new BsonDocument[]
        {
                new("$match", new BsonDocument("slug", new BsonDocument("$ne", excludeSlug))),
                new("$sample", new BsonDocument("size", count))
        };

        return await _postCollection.Aggregate<Post>(pipeline).ToListAsync();
    }
}