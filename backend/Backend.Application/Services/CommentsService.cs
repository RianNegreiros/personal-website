using Backend.Application.Models;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;

namespace Backend.Application.Services;

public class CommentsService : ICommentsService
{
    private readonly ICommentsRepository _commentsRepository;

    public CommentsService(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task<Comment> AddCommentToPost(PostViewModel post, CommentInputModel comment, User author)
    {
        Comment newComment = new()
        {
            PostId = post.Id,
            PostSlug = post.Slug,
            Author = author,
            Content = comment.Content,
            CreatedAt = DateTime.Now
        };

        return await _commentsRepository.AddComment(newComment);
    }

    public Task<List<Comment>> GetCommentsForPostByIdentifier(string identifier)
    {
        if (Guid.TryParse(identifier, out _))
        {
            // If identifier is a valid Guid, assume it's an ID
            return _commentsRepository.GetCommentsForPostById(identifier);
        }
        else
        {
            // If identifier is not a valid Guid, assume it's a slug
            return _commentsRepository.GetCommentsForPostBySlug(identifier);
        }
    }
}
