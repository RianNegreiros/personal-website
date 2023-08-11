using Backend.Application.Models;
using Backend.Core.Inferfaces.Repositories;
using Backend.Core.Models;

namespace Backend.Application.Services;

public class CommentsService : ICommentsService
{
    private readonly ICommentsRepository _commentsRepository;

    public CommentsService(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task<Comment> AddCommentToPost(string postId, CommentInputModel comment, User author)
    {
        var newComment = new Comment
        {
            Content = comment.Content,
            Author = author,
            PostId = postId,
            CreatedAt = DateTime.UtcNow
        };

        return await _commentsRepository.AddComment(newComment);
    }

    public async Task<List<Comment>> GetCommentsForPost(string postId)
    {
        return await _commentsRepository.GetCommentsForPost(postId);
    }
}
