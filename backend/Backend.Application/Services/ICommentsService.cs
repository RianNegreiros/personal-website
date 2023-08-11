using Backend.Core.Models;

namespace Backend.Application.Services;

public interface ICommentsService
{
    Task AddCommentToPost(string postId, Comment comment);
    Task<List<Comment>> GetCommentsForPost(string postId);
}
