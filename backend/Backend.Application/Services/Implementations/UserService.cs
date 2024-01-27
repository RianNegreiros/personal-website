using Backend.Application.Services.Interfaces;
using Backend.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Services.Implementations;

public class UserService : IUserService
{
  private readonly UserManager<User> _userManager;

  public UserService(UserManager<User> userManager)
  {
    _userManager = userManager;
  }

  public async Task<IEnumerable<User>> GetAllUsers()
  {
    return await _userManager.Users.ToListAsync();
  }

  public async Task<IdentityResult> CreateUser(string userName, string email, string password)
  {
    var user = new User
    {
      UserName = userName,
      Email = email
    };
    return await _userManager.CreateAsync(user, password);
  }

  public async Task<User> GetUserById(string userId)
  {
    return await _userManager.FindByIdAsync(userId);
  }

  public async Task<IdentityResult> UpdateUser(string userId, string? newUserName, string? newEmail)
  {
    var user = await _userManager.FindByIdAsync(userId);

    if (user == null)
    {
      return IdentityResult.Failed(new IdentityError { Description = "User not found." });
    }

    user.UserName = newUserName ?? user.UserName;
    user.Email = newEmail ?? user.Email;
    var result = await _userManager.UpdateAsync(user);

    return result;
  }

  public async Task DeleteUser(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId);
    if (user != null)
    {
      await _userManager.DeleteAsync(user);
    }
  }
}