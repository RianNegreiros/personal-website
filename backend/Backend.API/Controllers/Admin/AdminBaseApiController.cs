using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/[controller]")]
[ApiExplorerSettings(GroupName = "Admin")]
public class AdminBaseApiController : ControllerBase
{
  protected FluentValidation.Results.ValidationResult ValidateModel<TValidator, TModel>(TModel model)
    where TValidator : AbstractValidator<TModel>, new()
  {
    TValidator validator = new();
    return validator.Validate(model);
  }
}
