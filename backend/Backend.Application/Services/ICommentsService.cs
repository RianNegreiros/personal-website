using Backend.Application.Models;
using Backend.Core.Models;

namespace Backend.Application.Services;

public interface ICommentsService
{
    Task<Comment> AddCommentToPost(PostViewModel post, CommentInputModel comment, User author);
    Task<List<Comment>> GetCommentsForPostByIdentifier(string identifier);
}
