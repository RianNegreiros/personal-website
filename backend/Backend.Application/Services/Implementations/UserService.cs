using Backend.Application.Models.ViewModels;
using Backend.Application.Services.Interfaces;
using Backend.Core.Interfaces.Repositories;
using Backend.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Services.Implementations;

public class UserService : IUserService
{
  private readonly UserManager<User> _userManager;
  private readonly IPostRepository _postRepository;
  private readonly ICommentsRepository _commentsRepository;

  public UserService(UserManager<User> userManager, IPostRepository postRepository, ICommentsRepository commentsRepository)
  {
    _userManager = userManager;
    _postRepository = postRepository;
    _commentsRepository = commentsRepository;
  }

  public async Task<IEnumerable<AdminUserViewModel>> GetAllUsers()
  {
    var users = await _userManager.Users.ToListAsync();

    var adminUserViewModels = new List<AdminUserViewModel>();
    foreach (var user in users)
    {
      var userPosts = await _postRepository.GetPostsForUserById(user.Id);
      var userComments = await _commentsRepository.GetCommentsForUserById(user.Id);

      var adminUserViewModel = new AdminUserViewModel
      {
        Id = user.Id,
        Email = user.Email,
        Username = user.UserName,
        IsAdmin = await _userManager.IsInRoleAsync(user, "Admin"),
        Posts = userPosts.ToList(),
        Comments = userComments.ToList()
      };

      adminUserViewModels.Add(adminUserViewModel);
    }

    return adminUserViewModels;
  }

  public async Task<IdentityResult> CreateUser(string username, string email, string password, bool admin = false)
  {
    var user = new User
    {
      UserName = username,
      Email = email
    };

    var result = await _userManager.CreateAsync(user, password);

    if (admin)
    {
      await _userManager.AddToRoleAsync(user, "Admin");
    }

    return result;
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