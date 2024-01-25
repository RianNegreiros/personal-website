using System.Security.Claims;
using Backend.API.Helpers;
using Backend.API.Models;
using Backend.Application.Models;
using Backend.Application.Services;
using Backend.Application.Validators;
using Backend.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.API.Controllers;

public class UserController : BaseApiController
{
  private readonly UserManager<User> _userManager;
  private readonly SignInManager<User> _signInManager;
  private readonly ITokenService _tokenService;
  private readonly IConfiguration _config;

  public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IConfiguration config)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _tokenService = tokenService;
    _config = config;
  }

  [HttpPost("register")]
  [SwaggerOperation(Summary = "Register a new user.")]
  [ProducesResponseType(typeof(ApiResponse<UserViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

    User user = new()
    {
      Email = model.Email,
      UserName = model.Username
    };

    IdentityResult result = await _userManager.CreateAsync(user, model.Password);

    if (!result.Succeeded)
    {
      return BadRequest(result.Errors);
    }

    string token = _tokenService.GenerateJwtToken(user);

    return Ok(new ApiResponse<UserViewModel>
    {
      Success = true,
      Data = new UserViewModel
      {
        Id = user.Id,
        Username = user.UserName,
        Email = user.Email,
        Token = token
      }
    });
  }

  [HttpPost("login")]
  [SwaggerOperation(Summary = "Login a user.")]
  [ProducesResponseType(typeof(ApiResponse<UserViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<UserViewModel>> Login(LoginInputModel model)
  {
    FluentValidation.Results.ValidationResult validationResult = ValidateModel<LoginInputModelValidator, LoginInputModel>(model);

    if (!validationResult.IsValid)
    {
      return BadRequest(new ApiResponse<UserViewModel>
      {
        Success = false,
        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
      });
    }

    User user = await _userManager.FindByEmailAsync(model.Email);

    if (user == null)
    {
      return BadRequest("Invalid username or password.");
    }

    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

    if (!result.Succeeded)
    {
      return BadRequest("Invalid username or password.");
    }

    if (model.RememberMe)
    {
      await _userManager.UpdateAsync(user);
    }

    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

    string token = _tokenService.GenerateJwtToken(user, isAdmin);

    return Ok(new ApiResponse<UserViewModel>
    {
      Success = true,
      Data = new UserViewModel
      {
        Id = user.Id,
        Username = user.UserName,
        Email = user.Email,
        Token = token,
        IsAdmin = isAdmin
      }
    });
  }

  [Authorize]
  [HttpPost("logout")]
  [SwaggerOperation(Summary = "Logout a user.")]
  [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> Logout()
  {
    try
    {
      await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
    }
    catch (Exception)
    {
      return BadRequest(new ApiResponse<string>
      {
        Success = false,
        Errors = new List<string> { "Error logging out." }
      });
    }

    return Ok(new ApiResponse<string>
    {
      Success = true,
      Data = "Logged out."
    });
  }

  [HttpGet("me")]
  [SwaggerOperation(Summary = "Check if user is logged in.")]
  [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult<bool> GetCurrentUser()
  {
    bool? isAuthenticated = User?.Identity?.IsAuthenticated;
    return Ok(isAuthenticated ?? false);
  }

  [HttpGet("emailexists")]
  [SwaggerOperation(Summary = "Check if email exists.")]
  [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
  {
    return await _userManager.FindByEmailAsync(email) != null;
  }

  [Authorize]
  [HttpGet("isadmin")]
  [SwaggerOperation(Summary = "Check if user is admin.")]
  [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> IsAdmin()
  {
    string userEmail = User.FindFirstValue(ClaimTypes.Email);
    if (userEmail == null)
    {
      return Unauthorized();
    }

    User user = await _userManager.FindByEmailAsync(userEmail);

    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

    return Ok(isAdmin);
  }

  [HttpPost("autologin")]
  [SwaggerOperation(Summary = "Auto login a user.")]
  [ProducesResponseType(typeof(ApiResponse<UserViewModel>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<UserViewModel>> AutoLogin([FromBody] string token)
  {
    string? email = AutoLoginHelper.GetEmailFromValidToken(_config, token);
    if (email == null)
    {
      return Unauthorized();
    }

    User user = await _userManager.FindByEmailAsync(email);
    if (user == null)
    {
      return Unauthorized();
    }

    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

    return Ok(new ApiResponse<UserViewModel>
    {
      Success = true,
      Data = new UserViewModel
      {
        Id = user.Id,
        Username = user.UserName,
        Email = user.Email,
        Token = token,
        IsAdmin = isAdmin
      }
    });
  }
}