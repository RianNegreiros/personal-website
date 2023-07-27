using BlogBackend.Application.Services;
using BlogBackend.Core.Inferfaces.CloudServices;
using BlogBackend.Infrastructure.CloudServices;

namespace BlogBackend.API.Extensions;

public static class ApplicationServicesExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
  {
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IPostService, PostService>();
    services.AddScoped<ICloudinaryService, CloudinaryService>();

    return services;
  }
}