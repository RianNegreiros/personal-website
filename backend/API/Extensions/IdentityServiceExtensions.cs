using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend.Core.Models;
using Microsoft.EntityFrameworkCore;
using backend.Infrastructure.Data;

namespace backend.API.Extensions;

public static class IdentityServiceExtensions
{
  public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
  {
    var builder = services.AddIdentityCore<User>();

    builder = new IdentityBuilder(builder.UserType, builder.Services);
    builder.AddEntityFrameworkStores<IdentityDbContext>();
    builder.AddSignInManager<SignInManager<User>>();
    builder.AddDefaultTokenProviders();

    services.AddAuthentication(options => {
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
      });


    services.AddAuthentication(options =>
    {
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    })
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
    options.Cookie.Name = "token";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    });

    services.AddDbContext<IdentityDbContext>(opt =>
    {
      var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

      string connStr;

      if (env == "Development")
      {
        connStr = config.GetConnectionString("DefaultConnection");
      }
      else
      {
        connStr = config.GetConnectionString("IdentityConnection");
      }

      opt.UseNpgsql(connStr);
    });

    return services;
  }
}