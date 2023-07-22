using backend.Application.Services;
using backend.Core.Interfaces.Services;
using backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServicesExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
  {
    services.AddScoped<ITokenService, TokenService>();

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
        connStr = config["IdentityProductionConnection"];
      }

      opt.UseNpgsql(connStr);
    });

    return services;
  }
}