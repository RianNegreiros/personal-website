using System.Security.Claims;
using Backend.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Extensions;

public static class UserManagerExtensions
{
  public static async Task<User> FindByEmailFromClaimsPrinciple(this UserManager<User> input, ClaimsPrincipal user)
  {
    var email = user.FindFirstValue(ClaimTypes.Email);

    return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
  }
}