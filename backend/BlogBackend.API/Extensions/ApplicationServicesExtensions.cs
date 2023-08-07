using BlogBackend.Application.Services;
using BlogBackend.Core.Inferfaces.CloudServices;
using BlogBackend.Core.Inferfaces.Repositories;
using BlogBackend.Infrastructure.CloudServices;
using BlogBackend.Infrastructure.Repositories;

namespace BlogBackend.API.Extensions;

public static class ApplicationServicesExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IPostRepository, PostRepository>();

    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IPostService, PostService>();
    services.AddScoped<ICloudinaryService, CloudinaryService>();

    return services;
  }
}