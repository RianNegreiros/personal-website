using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using backend.Core.Models;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace backend.Persistence
{
  public class MongoDbContext : IdentityDbContext<User>
  {
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration) : base(new DbContextOptions<MongoDbContext>())
    {
      string connectionString = configuration.GetConnectionString("MongoDBConnection");
      var mongoClient = new MongoClient(connectionString);
      _database = mongoClient.GetDatabase(new MongoUrl(connectionString).DatabaseName);
    }
  }
}
