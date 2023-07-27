using System.Security.Claims;
using BlogBackend.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend.API.Extensions;

public static class UserManagerExtensions
{
  public static async Task<User> FindByEmailFromClaimsPrinciple(this UserManager<User> input, ClaimsPrincipal user)
  {
    var email = user.FindFirstValue(ClaimTypes.Email);

    return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
  }
}