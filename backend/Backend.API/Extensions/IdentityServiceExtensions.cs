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
      options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
      opt.SaveToken = true;
      opt.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtConfig:SecretKey"])),
        ValidIssuer = config["JwtConfig:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = false
      };
    })
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
      options.Cookie.Name = "token";
      options.Cookie.HttpOnly = true;
      options.ExpireTimeSpan = TimeSpan.FromDays(7);
      options.SlidingExpiration = true;
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
