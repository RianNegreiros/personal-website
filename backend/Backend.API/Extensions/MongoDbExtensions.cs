using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.API.Extensions;

public static class MongoDbExtensions
{
  public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration config)
  {
    string connectionString = config.GetConnectionString("MongoConnection");
    string databaseName = "BlogDB";

    MongoClient client = new(connectionString);
    IMongoDatabase database = client.GetDatabase(databaseName);

    if (!CollectionExists(database, "projects"))
    {
      database.CreateCollection("projects");
    }

    if (!CollectionExists(database, "posts"))
    {
      database.CreateCollection("posts");
    }

    if (!CollectionExists(database, "comments"))
    {
      database.CreateCollection("comments");
    }

    services.AddSingleton(database);

    return services;
  }

  private static bool CollectionExists(IMongoDatabase database, string collectionName)
  {
    BsonDocument filter = new("name", collectionName);
    IAsyncCursor<string> collections = database.ListCollectionNames(new ListCollectionNamesOptions { Filter = filter });
    return collections.Any();
  }
}