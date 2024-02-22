using Backend.API.Models;
using Backend.Application.Models;
using Backend.Application.Models.InputModels;
using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Application.Validators;
using Backend.Core.Interfaces.CloudServices;
using Backend.Core.Models;

using Hangfire;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers;

[Route("api/comments/{identifier}")]
public class CommentsController : BaseApiController
{
    private readonly ICommentsService _commentsService;
    private readonly IPostService _postService;
    private readonly UserManager<User> _userManager;
    private readonly INotificationService _notificationService;

    public CommentsController(ICommentsService commentsService, IPostService postService, UserManager<User> userManager, INotificationService notificationService)
    {
        _commentsService = commentsService;
        _postService = postService;
        _userManager = userManager;
        _notificationService = notificationService;
    }

    [Authorize]
    [HttpPost]
    [SwaggerOperation(Summary = "Add a comment to a post.")]
    [ProducesResponseType(typeof(ApiResponse<CommentViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddCommentToPost(string identifier, [FromBody] CommentInputModel model)
    {
        FluentValidation.Results.ValidationResult validationResult = ValidateModel<CommentInputModelValidator, CommentInputModel>(model);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponse<CommentViewModel>
            {
                Success = false,
                Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
            });

        PostViewModel? post = await _postService.GetPostByIdentifier(identifier);

        if (post == null)
        {
            return BadRequest(new ApiResponse<CommentViewModel>
            {
                Success = false,
                Errors = new List<string> { "Post not found." }
            });
        }

        var user = await _userManager.FindByIdAsync(model.AuthorId);

        Comment addedComment;

        addedComment = await _commentsService.AddCommentToPost(post, model, user);

        _notificationService.EnqueueNotification("CommentNotification", new NotificationContext
        {
            Title = user.UserName,
            UserEmail = user.Email,
            Content = addedComment.Content,
            PostSlug = addedComment.PostSlug,
        });
        return Ok(new ApiResponse<CommentViewModel>
        {
            Success = true,
            Data = new CommentViewModel
            {
                Id = addedComment.Id,
                PostId = addedComment.PostId,
                PostSlug = addedComment.PostSlug,
                Content = addedComment.Content,
                AuthorId = addedComment.Author.Id,
                AuthorEmail = addedComment.Author.Email,
                AuthorUsername = addedComment.Author.UserName,
                CreatedAt = addedComment.CreatedAt
            }
        });
    }

    [Authorize]
    [HttpPost("replies")]
    [SwaggerOperation(Summary = "Add a reply to a comment.")]
    [ProducesResponseType(typeof(ApiResponse<CommentViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddReplyToComment(string identifier, [FromBody] CommentInputModel model)
    {
        FluentValidation.Results.ValidationResult validationResult = ValidateModel<CommentInputModelValidator, CommentInputModel>(model);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponse<CommentViewModel>
            {
                Success = false,
                Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
            });

        CommentViewModel? comment = await _commentsService.GetCommentById(identifier);

        if (comment == null)
        {
            return BadRequest(new ApiResponse<CommentViewModel>
            {
                Success = false,
                Errors = new List<string> { "Comment not found." }
            });
        }

        var user = await _userManager.FindByIdAsync(model.AuthorId);

        Comment addedReply;

        addedReply = await _commentsService.AddReplyToComment(comment, model, user);

        _notificationService.EnqueueNotification("CommentReply", new NotificationContext
        {
            Title = user.UserName,
            UserEmail = user.Email,
            Content = addedReply.Content,
            PostSlug = addedReply.PostSlug,
        });

        return Ok(new ApiResponse<CommentViewModel>
        {
            Success = true,
            Data = new CommentViewModel
            {
                Id = addedReply.Id,
                PostId = addedReply.PostId,
                PostSlug = addedReply.PostSlug,
                Content = addedReply.Content,
                AuthorId = addedReply.Author.Id,
                AuthorEmail = addedReply.Author.Email,
                AuthorUsername = addedReply.Author.UserName,
                CreatedAt = addedReply.CreatedAt,
                UpdatedAt = addedReply.UpdatedAt
            }
        });
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all comments for a post.")]
    [ProducesResponseType(typeof(ApiResponse<List<CommentViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCommentsForPost(string identifier)
    {
        IEnumerable<Comment> comments = await _commentsService.GetCommentsForPostByIdentifier(identifier);

        List<CommentViewModel> commentsViewModel = comments.Select(c => new CommentViewModel
        {
            Id = c.Id,
            PostId = c.PostId,
            PostSlug = c.PostSlug,
            Content = c.Content,
            AuthorId = c.Author.Id,
            AuthorEmail = c.Author.Email,
            AuthorUsername = c.Author.UserName,
            Replies = c.Replies.Select(reply => new CommentViewModel
            {
                Id = reply.Id,
                AuthorId = reply.Author.Id,
                AuthorEmail = reply.Author.Email,
                AuthorUsername = reply.Author.UserName,
                Content = reply.Content,
                CreatedAt = reply.CreatedAt,
                UpdatedAt = reply.UpdatedAt
            }).ToList(),
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        }).ToList();

        return Ok(new ApiResponse<List<CommentViewModel>>
        {
            Success = true,
            Data = commentsViewModel
        });
    }
}