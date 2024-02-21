using FluentValidation;

using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "User")]
public class BaseApiController : ControllerBase
{
    protected FluentValidation.Results.ValidationResult ValidateModel<TValidator, TModel>(TModel model)
      where TValidator : AbstractValidator<TModel>, new()
    {
        TValidator validator = new();
        return validator.Validate(model);
    }
}