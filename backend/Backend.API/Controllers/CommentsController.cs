using Backend.API.Helpers;
using Backend.API.Models;
using Backend.Application.Models;
using Backend.Application.Services;
using Backend.Application.Validators;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

public class CommentsController : BaseApiController
{
    private readonly ICommentsService _commentsService;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    public CommentsController(ICommentsService commentsService, UserManager<User> userManager, IConfiguration config)
    {
        _commentsService = commentsService;
        _userManager = userManager;
        _config = config;
    }

    [Authorize]
    [HttpPost("{postId}")]
    public async Task<IActionResult> AddCommentToPost(string postId, [FromBody] CommentInputModel comment)
    {
        var validationResult = ValidateModel<CommentInputModelValidator, CommentInputModel>(comment);

        if (!validationResult.IsValid)
            return BadRequest(new ApiResponse<CommentViewModel>
            {
                Success = false,
                Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
            });

        var email = AutoLoginHelper.GetEmailFromValidToken(_config, comment.token);
        if (email == null)
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Unauthorized();
        }

        Comment addedComment = await _commentsService.AddCommentToPost(postId, comment, user);

        var commentViewModel = new CommentViewModel
        {
            Id = addedComment.Id,
            Content = addedComment.Content,
            Author = addedComment.Author,
            CreatedAt = addedComment.CreatedAt
        };
        return Ok(commentViewModel);
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetCommentsForPost(string postId)
    {
        var comments = await _commentsService.GetCommentsForPost(postId);

        List<CommentViewModel> commentsViewModel = comments.Select(c => new CommentViewModel
        {
            Id = c.Id,
            Content = c.Content,
            Author = c.Author,
            CreatedAt = c.CreatedAt
        }).ToList();

        return Ok(commentsViewModel);
    }
}