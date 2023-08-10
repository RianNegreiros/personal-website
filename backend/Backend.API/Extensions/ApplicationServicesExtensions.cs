using Backend.Application.Services;
using Backend.Core.CloudServices;
using Backend.Core.Inferfaces;
using Backend.Infrastructure.CloudServices;
using Backend.Infrastructure.Repositories;

namespace Backend.API.Extensions;

public static class ApplicationServicesExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    services.AddScoped<IPostRepository, PostRepository>();
    services.AddScoped<IProjectsRepository, ProjectsRepository>();

    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IPostService, PostService>();
    services.AddScoped<ICloudinaryService, CloudinaryService>();
    services.AddScoped<IProjectsService, ProjectsService>();

    return services;
  }
}