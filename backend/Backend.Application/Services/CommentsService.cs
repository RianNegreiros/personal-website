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

    public async Task<Comment> AddCommentToPostById(string postId, CommentInputModel comment, User author)
    {
        Comment newComment = new()
        {
            Content = comment.Content,
            Author = author,
            PostId = postId,
            CreatedAt = DateTime.UtcNow
        };

        return await _commentsRepository.AddComment(newComment);
    }

    public async Task<Comment> AddCommentToPostBySlug(string postSlug, CommentInputModel comment, User author)
    {
        Comment newComment = new()
        {
            Content = comment.Content,
            Author = author,
            PostSlug = postSlug,
            CreatedAt = DateTime.UtcNow
        };

        return await _commentsRepository.AddComment(newComment);
    }

    public async Task<List<Comment>> GetCommentsForPostById(string postId)
    {
        return await _commentsRepository.GetCommentsForPostById(postId);
    }

    public async Task<List<Comment>> GetCommentsForPostBySlug(string postSlug)
    {
        return await _commentsRepository.GetCommentsForPostBySlug(postSlug);
    }
}
