namespace Backend.API.Extensions;

public static class HealthChecksExtensions
{
    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("IdentityConnection"),
                name: "PostgreSQL", tags: new string[] { "database", "users" })
            .AddMongoDb(configuration.GetConnectionString("MongoConnection"),
                name: "MongoDB", tags: new string[] { "database", "posts", "comments", "projects" })
            .AddRedis(configuration.GetConnectionString("Redis"),
                name: "Redis", tags: new string[] { "database", "cache" });

        return services;
    }
}
