using Backend.Application.Models.InputModels;
using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/[controller]")]
public class UsersController : ControllerBase
{
  private readonly IUserService _userService;

  public UsersController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<AdminUserViewModel>>> GetAll()
  {
    var users = await _userService.GetAllUsers();
    return Ok(users);
  }

  [HttpGet("{id}")]
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
  public async Task<IActionResult> Delete(string id)
  {
    await _userService.DeleteUser(id);
    return NoContent();
  }
}