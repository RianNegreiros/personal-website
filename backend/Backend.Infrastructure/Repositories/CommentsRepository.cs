using Backend.Core.Inferfaces.Repositories;
using Backend.Core.Models;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class CommentsRepository : ICommentsRepository
{
    private readonly IMongoCollection<Comment> _comments;

    public CommentsRepository(IMongoDatabase database)
    {
        _comments = database.GetCollection<Comment>("comments");
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        await _comments.InsertOneAsync(comment);
        return comment;
    }

    public async Task<List<Comment>> GetCommentsForPost(string postId)
    {
        return await _comments.Find(c => c.PostId == postId).ToListAsync();
    }
}
