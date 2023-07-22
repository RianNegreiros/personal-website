using backend.API.DTOs;
using backend.Core.Interfaces.Services;
using backend.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Persistence.Services;

public class UserService : IUserService
{
  private readonly UserManager<User> _userManager;
  private readonly ITokenService _tokenService;

  public UserService(UserManager<User> userManager, ITokenService tokenService)
  {
    _userManager = userManager;
    _tokenService = tokenService;
  }

  public async Task<UserDto> GetCurrentUser(string userEmail)
  {
    var user = await _userManager.FindByEmailAsync(userEmail);

    return new UserDto
    {
      Id = user.Id,
      Username = user.UserName,
      Token = _tokenService.GenerateJwtToken(user),
      Email = user.Email
    };
  }

  public async Task<bool> CheckEmailExists(string email)
  {
    return await _userManager.FindByEmailAsync(email) != null;
  }
}