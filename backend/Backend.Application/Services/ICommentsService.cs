using Backend.Application.Models;
using Backend.Core.Models;

namespace Backend.Application.Services;

public interface ICommentsService
{
    Task<Comment> AddCommentToPost(string postId, CommentInputModel comment, User author);
    Task<List<Comment>> GetCommentsForPost(string postId);
}
