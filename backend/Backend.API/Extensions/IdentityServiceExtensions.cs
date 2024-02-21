using System.Text;

using Backend.Core.Models;
using Backend.Infrastructure.Data;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        IdentityBuilder builder = services.AddIdentity<User, IdentityRole>();

        builder.AddEntityFrameworkStores<IdentityDbContext>();
        builder.AddSignInManager<SignInManager<User>>();
        builder.AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
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
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["token"];
                return Task.CompletedTask;
            }
            };
        })
        .AddCookie(options =>
            {
                options.Cookie.Name = "token";
            });

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.None;

            var environment = config.GetValue<string>(WebHostDefaults.EnvironmentKey);
            if (environment == Environments.Development)
            {
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = CookieSecurePolicy.None;
            }
            else
            {
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
            }
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        });

        services.AddDbContext<IdentityDbContext>(opt =>
        {
            string connStr = config.GetConnectionString("IdentityConnection");
            opt.UseNpgsql(connStr);
        });

        return services;
    }
}