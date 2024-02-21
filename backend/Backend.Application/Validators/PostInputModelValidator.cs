using Backend.Application.Models.InputModels;

using FluentValidation;

namespace Backend.Application.Validators;

public class PostInputModelValidator : AbstractValidator<PostInputModel>
{
    public PostInputModelValidator()
    {
        RuleFor(model => model.Title).NotEmpty().Length(4, 100).WithMessage("Title must be between 4 and 100 characters.");
        RuleFor(model => model.Summary).NotEmpty().Length(10, 300).WithMessage("Summary must be between 10 and 300 characters.");
        RuleFor(model => model.Content).NotEmpty().WithMessage("Content is required.");
    }
}