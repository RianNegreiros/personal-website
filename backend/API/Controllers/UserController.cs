using backend.API.DTOs;
using backend.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterDto registerDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    if (await _userService.RegisterUser(registerDto))
    {
      return Ok(new { message = "Registration successful." });
    }
    else
    {
      return BadRequest(new { message = "Username already taken." });
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginDto loginDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var token = await _userService.Login(loginDto);
    if (token == null)
    {
      return Unauthorized();
    }

    return Ok(new { token });
  }
}