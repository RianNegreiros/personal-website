using BlogBackend.Application.Models;
using FluentValidation;

namespace BlogBackend.Application.Validators;

public class PostInputModelValidator : AbstractValidator<PostInputModel>
{
    public PostInputModelValidator()
    {
        RuleFor(model => model.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(model => model.Summary).NotEmpty().WithMessage("Summary is required.");
        RuleFor(model => model.Content).NotEmpty().WithMessage("Content is required.");
    }
}
