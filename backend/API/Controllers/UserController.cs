using System.Security.Claims;
using backend.API.DTOs;
using backend.Core.Interfaces.Services;
using backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly UserManager<User> _userManager;

  public UserController(UserManager<User> userManager)
  {
    _userManager = userManager;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterDto registerDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var user = new User
    {
      UserName = registerDto.Username
    };

    var result = await _userManager.CreateAsync(user, registerDto.Password);

    if (!result.Succeeded)
    {
      return BadRequest(result.Errors);
    }

    return Ok(new { message = "Registration successful." });
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
      return Unauthorized(new { message = "Invalid username or password." });
    }

    Response.Cookies.Append("token", token, new CookieOptions { HttpOnly = true });

    return Ok(new { token });
  }

  [Authorize]
  [HttpGet("logout")]
  public IActionResult Logout()
  {
    Response.Cookies.Append("token", "", new CookieOptions { HttpOnly = true });
    return Ok(new { message = "Logout successful." });
  }

  [Authorize]
  [HttpGet("profile")]
  public IActionResult Profile()
  {
    var username = User.Identity.Name;
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    return Ok(new { Username = username, UserId = userId });
  }
}