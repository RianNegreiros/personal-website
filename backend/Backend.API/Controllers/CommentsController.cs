using Backend.API.Models;
using Backend.Application.Models.InputModels;
using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Application.Validators;
using Backend.Core.Models;
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

    public CommentsController(ICommentsService commentsService, IPostService postService, UserManager<User> userManager)
    {
        _commentsService = commentsService;
        _postService = postService;
        _userManager = userManager;
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

        var user = await _userManager.FindByIdAsync(model.Id);

        Comment addedComment;

        addedComment = await _commentsService.AddCommentToPost(post, model, user);

        return Ok(new ApiResponse<CommentViewModel>
        {
            Success = true,
            Data = new CommentViewModel
            {
                Id = addedComment.Id,
                Content = addedComment.Content,
                Author = addedComment.Author,
                CreatedAt = addedComment.CreatedAt
            }
        });
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all comments for a post.")]
    [ProducesResponseType(typeof(ApiResponse<List<CommentViewModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCommentsForPost(string identifier)
    {
        IEnumerable<Comment> comments;

        comments = await _commentsService.GetCommentsForPostByIdentifier(identifier);

        List<CommentViewModel> commentsViewModel = comments.Select(c => new CommentViewModel
        {
            Id = c.Id,
            Content = c.Content,
            Author = c.Author,
            CreatedAt = c.CreatedAt
        }).ToList();

        return Ok(new ApiResponse<List<CommentViewModel>>
        {
            Success = true,
            Data = commentsViewModel
        });
    }
}
