using BlogBackend.Application.Models;
using FluentValidation;

namespace BlogBackend.Application.Validators;

public class PostInputModelValidator : AbstractValidator<PostInputModel>
{
    public PostInputModelValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Summary).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Content).NotEmpty();
    }
}