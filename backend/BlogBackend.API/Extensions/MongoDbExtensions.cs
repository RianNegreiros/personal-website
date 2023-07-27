using MongoDB.Driver;

namespace BlogBackend.API.Extensions;

public static class MongoDbExtensions
{
  public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration config)
  {
    var connectionString = config.GetConnectionString("MongoConnection");
    var databaseName = "BlogDB";

    var client = new MongoClient(connectionString);
    var database = client.GetDatabase(databaseName);

    services.AddSingleton(database);

    return services;
  }
}