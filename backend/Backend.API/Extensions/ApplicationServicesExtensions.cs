using Backend.Application.Services.Implementations;
using Backend.Application.Services.Interfaces;
using Backend.Core.Interfaces.CloudServices;
using Backend.Core.Interfaces.Repositories;
using Backend.Infrastructure.Caching;
using Backend.Infrastructure.CloudServices;
using Backend.Infrastructure.Repositories;

namespace Backend.API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IProjectsRepository, ProjectsRepository>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<ICommentsService, CommentsService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFeedService, FeedService>();

        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<ICachingService, CachingService>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}