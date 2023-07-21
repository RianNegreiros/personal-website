using backend.API.DTOs;
using backend.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (await _userService.RegisterUser(registerDto))
        {
            return Ok("Registration successful");
        }
        return BadRequest("Username already exists or registration failed");
    }
}