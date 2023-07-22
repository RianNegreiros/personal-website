using API.Extensions;
using backend.API.DTOs;
using backend.Core.Interfaces.Services;
using backend.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers;

public class UserController : BaseApiController
{
  private readonly UserManager<User> _userManager;
  private readonly SignInManager<User> _signInManager;
  private readonly ITokenService _tokenService;
  private readonly IUserService _userService;

  public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IUserService userService)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _tokenService = tokenService;
    _userService = userService;
  }

  [HttpPost("register")]
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    if (CheckEmailExists(registerDto.Email).Result.Value)
    {
      return BadRequest("Email address is in use.");
    }

    var user = new User
    {
      Email = registerDto.Email,
      UserName = registerDto.Username
    };

    var result = await _userManager.CreateAsync(user, registerDto.Password);

    if (!result.Succeeded)
    {
      return BadRequest(result.Errors);
    }

    return new UserDto
    {
      Username = user.UserName,
      Token = _tokenService.GenerateJwtToken(user),
      Email = user.Email
    };
  }

  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var user = await _userManager.FindByEmailAsync(loginDto.Email);

    if (user == null)
    {
      return BadRequest("Invalid username or password.");
    }

    var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, true);

    if (!result.Succeeded)
    {
      return BadRequest("Invalid username or password.");
    }

    var token = _tokenService.GenerateJwtToken(user);

    return Ok(new UserDto
    {
      Username = user.UserName,
      Token = token,
      Email = user.Email
    });
  }

  [Authorize]
  [HttpPost("logout")]
  public async Task<IActionResult> Logout()
  {
    try
    {
      await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
    }
    catch (Exception)
    {
      return BadRequest(new { message = "Error logging out." });
    }

    return Ok(new { message = "User logged out successfully." });
  }

  [Authorize]
  [HttpGet]
  public async Task<ActionResult<UserDto>> GetCurrentUser()
  {
    var user = await _userManager.FindByEmailFromClaimsPrinciple(User);

    return await _userService.GetCurrentUser(user.Email);
  }

  [HttpGet("emailexists")]
  public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
  {
    return await _userService.CheckEmailExists(email);
  }
}