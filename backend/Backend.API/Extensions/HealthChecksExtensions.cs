namespace Backend.API.Extensions;

public static class HealthChecksExtensions
{
    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("IdentityConnection"),
                name: "PostgreSQL", tags: new string[] { "db", "data" })
            .AddMongoDb(configuration.GetConnectionString("MongoConnection"),
                name: "MongoDB", tags: new string[] { "db", "data" })
            .AddRedis(configuration.GetConnectionString("Redis"),
                name: "Redis", tags: new string[] { "db", "data" });

        return services;
    }
}
