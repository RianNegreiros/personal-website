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

    public async Task AddCommentToPost(string postId, Comment comment)
    {
        comment.PostId = postId;
        await _commentsRepository.AddComment(comment);
    }

    public async Task<List<Comment>> GetCommentsForPost(string postId)
    {
        return await _commentsRepository.GetCommentsForPost(postId);
    }
}
