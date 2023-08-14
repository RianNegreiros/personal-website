using Backend.Application.Models;
using Backend.Core.Models;

namespace Backend.Application.Services;

public interface ICommentsService
{
    Task<Comment> AddCommentToPostById(string postId, CommentInputModel comment, User author);
    Task<Comment> AddCommentToPostBySlug(string postSlug, CommentInputModel comment, User author);
    Task<List<Comment>> GetCommentsForPostById(string postId);
    Task<List<Comment>> GetCommentsForPostBySlug(string postSlug);
}
