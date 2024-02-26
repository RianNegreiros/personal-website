namespace Backend.API.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(configuration["ClientUrl"] ?? "http://client:3000")));

        return services;
    }
}