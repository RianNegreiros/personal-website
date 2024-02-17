using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories;

public class CommentsRepository : ICommentsRepository
{
    private readonly IMongoCollection<Comment> _comments;

    public CommentsRepository(IMongoDatabase database)
    {
        _comments = database.GetCollection<Comment>("comments");
    }

    public async Task<List<Comment>> GetAll()
    {
        return await _comments.Find(_ => true).ToListAsync();
    }

    public async Task<Comment> GetById(string id)
    {
        return await _comments.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        await _comments.InsertOneAsync(comment);
        return comment;
    }

    public async Task<Comment> AddReplyToComment(string commentId, Comment reply)
    {
        reply.Id = ObjectId.GenerateNewId().ToString();

        var filter = Builders<Comment>.Filter.Eq(c => c.Id, commentId);
        var update = Builders<Comment>.Update
            .Push(c => c.Replies, reply)
            .CurrentDate(c => c.UpdatedAt);
        var options = new FindOneAndUpdateOptions<Comment> { ReturnDocument = ReturnDocument.After };

        Comment updatedComment = await _comments.FindOneAndUpdateAsync(filter, update, options);

        return updatedComment;
    }

    public async Task<List<Comment>> GetCommentsForUserById(string userId)
    {
        return await _comments.Find(c => c.Author.Id == userId).ToListAsync();
    }

    public async Task<List<Comment>> GetCommentsForPostById(string postId)
    {
        return await _comments.Find(c => c.PostId == postId).ToListAsync();
    }

    public async Task<List<Comment>> GetCommentsForPostBySlug(string postSlug)
    {
        return await _comments.Find(c => c.PostSlug == postSlug).ToListAsync();
    }

    public async Task Delete(string id)
    {
        var filter = Builders<Comment>.Filter.Eq("Id", id);

        await _comments.DeleteOneAsync(filter);
    }
}
