using Backend.Core.Models;

namespace Backend.Core.Inferfaces.Repositories;

public interface ICommentsRepository
{
    Task<Comment> AddComment(Comment comment);
    Task<List<Comment>> GetCommentsForPost(string postId);
}