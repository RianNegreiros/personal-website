using BlogBackend.Application.Services;
using BlogBackend.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogBackend.Application.Services;

public class UserService : IUserService
{
  private readonly UserManager<User> _userManager;
  private readonly ITokenService _tokenService;

  public UserService(UserManager<User> userManager, ITokenService tokenService)
  {
    _userManager = userManager;
    _tokenService = tokenService;
  }

  public async Task<User> GetCurrentUser(string userEmail)
  {
    return await _userManager.FindByEmailAsync(userEmail);
  }

  public async Task<bool> CheckEmailExists(string email)
  {
    return await _userManager.FindByEmailAsync(email) != null;
  }
}