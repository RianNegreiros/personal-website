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
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
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

    var user = new User
    {
      Email = model.Email,
      UserName = model.Username,
      PersistentToken = ""
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
  [SwaggerOperation(Summary = "Login a user.")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<UserViewModel>> Login(LoginInputModel model)
  {
    var validationResult = ValidateModel<LoginInputModelValidator, LoginInputModel>(model);

    if (!validationResult.IsValid)
    {
      return BadRequest(new ApiResponse<UserViewModel>
      {
        Success = false,
        Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
      });
    }

    var user = await _userManager.FindByEmailAsync(model.Email);

    if (user == null)
    {
      return BadRequest("Invalid username or password.");
    }


    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

    if (!result.Succeeded)
    {
      return BadRequest("Invalid username or password.");
    }

    if (model.RememberMe)
    {
      user.PersistentToken = Guid.NewGuid().ToString();
      await _userManager.UpdateAsync(user);
    }
    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

    var token = _tokenService.GenerateJwtToken(user, isAdmin);


    return Ok(new UserViewModel
    {
      Id = user.Id,
      Username = user.UserName,
      Token = token,
      Email = user.Email,
      IsAdmin = isAdmin,
      RememberMe = model.RememberMe
    });
  }

  [Authorize]
  [HttpPost("logout")]
  [SwaggerOperation(Summary = "Logout a user.")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
      return BadRequest(new { message = "Error logging out." });
    }

    return Ok(new { message = "User logged out successfully." });
  }

  [HttpGet("me")]
  [SwaggerOperation(Summary = "Check if user is logged in.")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public ActionResult<bool> GetCurrentUser()
  {
    return Ok(User.Identity.IsAuthenticated);
  }

  [HttpGet("emailexists")]
  [SwaggerOperation(Summary = "Check if email exists.")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<bool>> CheckEmailExists([FromQuery] string email)
  {
    return await _userManager.FindByEmailAsync(email) != null;
  }

  [Authorize]
  [HttpGet("isadmin")]
  [SwaggerOperation(Summary = "Check if user is admin.")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> IsAdmin()
  {
    var userEmail = User.FindFirstValue(ClaimTypes.Email);
    if (userEmail == null)
    {
      return Unauthorized();
    }

    var user = await _userManager.FindByEmailAsync(userEmail);

    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

    return Ok(isAdmin);
  }

  [HttpPost("autologin")]
  [SwaggerOperation(Summary = "Auto login a user.")]
  [Consumes("application/json")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<UserViewModel>> AutoLogin([FromBody] string token)
  {
    var email = AutoLoginHelper.GetEmailFromValidToken(_config, token);
    if (email == null)
    {
      return Unauthorized();
    }

    var user = await _userManager.FindByEmailAsync(email);
    if (user == null)
    {
      return Unauthorized();
    }

    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

    return Ok(new UserViewModel
    {
      Id = user.Id,
      Username = user.UserName,
      Email = user.Email,
      IsAdmin = isAdmin
    });
  }
}