using Resend;

namespace Backend.API.Extensions;

public static class ResendExtensions
{
    public static IServiceCollection AddResend(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.AddHttpClient<ResendClient>();
        services.Configure<ResendClientOptions>(options => options.ApiToken = configuration.GetValue<string>("ResendKey"));
        services.AddTransient<IResend, ResendClient>();

        return services;
    }
}