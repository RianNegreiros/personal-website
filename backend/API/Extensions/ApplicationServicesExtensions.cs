using backend.Core.Interfaces.Services;
using backend.Infrastructure.Services;

namespace backend.API.Extensions;

public static class ApplicationServicesExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
  {
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IPostService, PostService>();
    services.AddScoped<CloudinaryService>();

    return services;
  }
}