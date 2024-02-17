using Backend.Application.Models.InputModels;
using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using MongoDB.Driver.Core.WireProtocol.Messages;

namespace Backend.Application.Services.Implementations;

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
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        return await _commentsRepository.AddComment(newComment);
    }

    public async Task<Comment> AddReplyToComment(CommentViewModel comment, CommentInputModel model, User author)
    {
        Comment reply = new()
        {
            PostId = comment.PostId,
            PostSlug = comment.PostSlug,
            Author = author,
            Content = model.Content,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        return await _commentsRepository.AddReplyToComment(comment.Id, reply);
    }

    public async Task<List<Comment>> GetCommentsForPostByIdentifier(string identifier)
    {
        if (Guid.TryParse(identifier, out _))
        {
            // If identifier is a valid Guid, assume it's an ID
            return await _commentsRepository.GetCommentsForPostById(identifier);
        }
        else
        {
            // If identifier is not a valid Guid, assume it's a slug
            return await _commentsRepository.GetCommentsForPostBySlug(identifier);
        }
    }

    public async Task<CommentViewModel> GetCommentById(string id)
    {
        Comment comment = await _commentsRepository.GetById(id);

        return new CommentViewModel
        {
            Id = comment.Id,
            PostId = comment.PostId,
            PostSlug = comment.PostSlug,
            AuthorEmail = comment.Author.Email,
            AuthorUsername = comment.Author.UserName,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt
        };
    }
}
