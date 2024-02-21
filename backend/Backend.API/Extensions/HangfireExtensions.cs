using Hangfire;
using Hangfire.MemoryStorage;

namespace Backend.API.Extensions;

public static class HangfireExtensions
{
    public static IServiceCollection AddHangfire(this IServiceCollection services)
    {
        services.AddHangfire(config =>
        {
            config.UseMemoryStorage();
        });
        services.AddHangfireServer();

        return services;
    }
}