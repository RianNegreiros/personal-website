using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.API.Extensions;

public static class MongoDbExtensions
{
  public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration config)
  {
    var connectionString = config.GetConnectionString("MongoConnection");
    var databaseName = "BlogDB";

    var client = new MongoClient(connectionString);
    var database = client.GetDatabase(databaseName);

    if (!CollectionExists(database, "projects"))
    {
      database.CreateCollection("projects");
    }

    if (!CollectionExists(database, "posts"))
    {
      database.CreateCollection("posts");
    }

    services.AddSingleton(database);

    return services;
  }

  private static bool CollectionExists(IMongoDatabase database, string collectionName)
  {
    var filter = new BsonDocument("name", collectionName);
    var collections = database.ListCollectionNames(new ListCollectionNamesOptions { Filter = filter });
    return collections.Any();
  }
}