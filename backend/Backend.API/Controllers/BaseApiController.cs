using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
  protected FluentValidation.Results.ValidationResult ValidateModel<TValidator, TModel>(TModel model)
    where TValidator : AbstractValidator<TModel>, new()
  {
    var validator = new TValidator();
    return validator.Validate(model);
  }
}
