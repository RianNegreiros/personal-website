using Backend.Application.Models.InputModels;
using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers.Admin;

[SwaggerTag("To admins manage users in the system.")]
public class UsersController : AdminBaseApiController
{
  private readonly IUserService _userService;

  public UsersController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all users.")]
  [ProducesResponseType(typeof(IEnumerable<AdminUserViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<IEnumerable<AdminUserViewModel>>> GetAll()
  {
    var users = await _userService.GetAllUsers();
    return Ok(users);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get a user by ID.")]
  [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<User>> GetById(string id)
  {
    var user = await _userService.GetUserById(id);

    if (user == null)
    {
      return NotFound();
    }

    return Ok(user);
  }

  [HttpPost]
  [SwaggerOperation(Summary = "Create a new user.")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Create([FromBody] AdminCreateUserInputModel model)
  {
    var result = await _userService.CreateUser(model.Username, model.Email, model.Password, model.Admin);

    if (result.Succeeded)
    {
      return Ok("User created successfully");
    }
    else
    {
      return BadRequest(result.Errors);
    }
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update a user.")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Update(string id, string? newUserName, string? newEmail)
  {
    var result = await _userService.UpdateUser(id, newUserName, newEmail);
    if (result.Succeeded)
    {
      return Ok("User updated successfully");
    }
    else
    {
      return BadRequest(result.Errors);
    }
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete a user.")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Delete(string id)
  {
    await _userService.DeleteUser(id);
    return NoContent();
  }
}