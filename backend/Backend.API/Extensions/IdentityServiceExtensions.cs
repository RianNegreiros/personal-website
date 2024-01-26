using Microsoft.AspNetCore.Identity;
using System.Text;
using Backend.Core.Models;
using Backend.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Extensions;

public static class IdentityServiceExtensions
{
  public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
  {
    IdentityBuilder builder = services.AddIdentityCore<User>();

    builder = new IdentityBuilder(builder.UserType, builder.Services);
    builder.AddRoles<IdentityRole>();
    builder.AddEntityFrameworkStores<IdentityDbContext>();
    builder.AddSignInManager<SignInManager<User>>();
    builder.AddDefaultTokenProviders();

    services.AddAuthentication(options =>
    {
      options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtConfig:SecretKey"])),
        ValidIssuer = config["JwtConfig:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = false
      };
    });

    services.AddAuthorization(options =>
    {
      options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    });

    services.AddDbContext<IdentityDbContext>(opt =>
    {
      string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      string connStr = config.GetConnectionString("IdentityConnection");

      opt.UseNpgsql(connStr);
    });

    return services;
  }
}
