using Backend.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Backend.Infrastructure.Data.SeedData
{
    public class AdminUserSeed
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            User adminUser = new()
            {
                UserName = configuration["AdminUser:UserName"],
                Email = configuration["AdminUser:Email"],
                EmailConfirmed = true
            };

            if (await userManager.FindByEmailAsync(configuration["AdminUser:Email"]) == null)
            {
                await userManager.CreateAsync(adminUser, configuration["AdminUser:Password"]);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}