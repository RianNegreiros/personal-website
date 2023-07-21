using backend.Core.Models;
using MongoDB.Driver;

namespace backend.Persistence;

public class MongoDbContext
{
  private readonly IMongoDatabase _database;

  public MongoDbContext(IConfiguration configuration)
  {
    string connectionString = configuration.GetConnectionString("MongoDBConnection");

    var mongoClient = new MongoClient(connectionString);

    _database = mongoClient.GetDatabase(new MongoUrl(connectionString).DatabaseName);
  }

  public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}