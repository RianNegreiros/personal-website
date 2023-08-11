using Backend.Application.Models;
using FluentValidation;

namespace Backend.Application.Validators;

public class LoginInputModelValidator : AbstractValidator<LoginInputModel>
{
    public LoginInputModelValidator()
    {
        RuleFor(model => model.Email).NotEmpty().EmailAddress();
        RuleFor(model => model.Password).NotEmpty().MinimumLength(12);
    }
}
