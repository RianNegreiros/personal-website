using BlogBackend.Application.Models;
using BlogBackend.Application.Services;
using BlogBackend.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackend.API.Controllers;

public class UserController : BaseApiController
{
  private readonly UserManager<User> _userManager;
  private readonly SignInManager<User> _signInManager;
  private readonly ITokenService _tokenService;

  public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _tokenService = tokenService;
  }

  [HttpPost("register")]
  public async Task<ActionResult<UserViewModel>> Register(RegisterInputModel model)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    if (CheckEmailExists(model.Email).Result.Value)
    {
      return BadRequest("Email address is in use.");
    }

    var user = new User
    {
      Email = model.Email,
      UserName = model.Username
    };

    var result = await _userManager.CreateAsync(user, model.Password);

    if (!result.Succeeded)
    {
      return BadRequest(result.Errors);
    }

    return new UserViewModel
    {
      Id = user.Id,
      Username = user.UserName,
      Token = _tokenService.GenerateJwtToken(user),
      Email = user.Email
    };
  }

  [HttpPost("login")]
  public async Task<ActionResult<UserViewModel>> Login(LoginInputModel model)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var user = await _userManager.FindByEmailAsync(model.Email);

    if (user == null)
    {
      return BadRequest("Invalid username or password.");
    }

    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

    if (!result.Succeeded)
    {
      return BadRequest("Invalid username or password.");
    }

    var token = _tokenService.GenerateJwtToken(user);

    return Ok(new UserViewModel
    {
      Id = user.Id,
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

  [HttpGet("me")]
  public ActionResult<bool> GetCurrentUser()
  {
    var isAuthenticated = User.Identity.IsAuthenticated;

    return Ok(isAuthenticated);
  }

  [HttpGet("emailexists")]
  public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
  {
    return await _userManager.FindByEmailAsync(email) != null;
  }
}