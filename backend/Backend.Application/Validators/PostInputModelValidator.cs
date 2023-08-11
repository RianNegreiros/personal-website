using Backend.Application.Models;
using FluentValidation;

namespace Backend.Application.Validators;

public class PostInputModelValidator : AbstractValidator<PostInputModel>
{
    public PostInputModelValidator()
    {
        RuleFor(model => model.Title).NotEmpty().Length(4, 50).WithMessage("Title must be between 4 and 50 characters.");
        RuleFor(model => model.Summary).NotEmpty().Length(10, 200).WithMessage("Summary must be between 10 and 200 characters.");
        RuleFor(model => model.Content).NotEmpty().WithMessage("Content is required.");
    }
}
