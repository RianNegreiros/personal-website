using Backend.Application.Models;
using Backend.Application.Models.InputModels;

using FluentValidation;

namespace Backend.Application.Validators;

public class ProjectInputModelValidator : AbstractValidator<ProjectInputModel>
{
    public ProjectInputModelValidator()
    {
        RuleFor(model => model.Title).NotEmpty().Length(4, 100);
        RuleFor(model => model.Url).NotEmpty();
        RuleFor(model => model.Overview).NotEmpty().Length(10, 500);
    }
}