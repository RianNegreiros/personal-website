using Backend.Application.Models;
using FluentValidation;

namespace Backend.Application.Validators;

public class UpdatePostModelValidator : AbstractValidator<UpdatePostModel>
{
    public UpdatePostModelValidator()
    {
        RuleFor(model => model.Title).Length(4, 100).WithMessage("Title must be between 4 and 100 characters.");
        RuleFor(model => model.Summary).Length(4, 100).WithMessage("Summary must be between 4 and 100 characters.");
    }
}
