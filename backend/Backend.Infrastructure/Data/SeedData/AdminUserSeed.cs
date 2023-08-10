using Backend.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Backend.Infrastructure.Data.SeedData
{
    public class AdminUserSeed
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            }
            if (userManager.FindByEmailAsync(configuration["AdminUser:Email"]).Result == null)
            {
                var adminUser = new User
                {
                    UserName = configuration["AdminUser:UserName"],
                    Email = configuration["AdminUser:Email"],
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, configuration["AdminUser:Password"]);
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}