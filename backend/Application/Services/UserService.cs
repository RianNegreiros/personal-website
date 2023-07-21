using backend.Core.Models;
using backend.API.DTOs;
using MongoDB.Driver;
using backend.Core.Interfaces.Services;
using backend.Persistence;

namespace backend.Application.Services;

public class UserService : IUserService
{
  private readonly IMongoCollection<User> _usersCollection;

  public UserService(MongoDbContext dbContext)
  {
    _usersCollection = dbContext.Users;
  }

  public async Task<bool> IsUsernameTaken(string username)
  {
    return await _usersCollection.Find(u => u.Username == username).AnyAsync();
  }

  public async Task<bool> RegisterUser(RegisterDto registerDto)
  {
    if (await IsUsernameTaken(registerDto.Username))
    {
      return false;
    }

    var user = new User
    {
      Username = registerDto.Username,
      PasswordHash = HashPassword(registerDto.Password)
    };

    await _usersCollection.InsertOneAsync(user);

    return true;
  }

  private string HashPassword(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(password);
  }

  private bool VerifyPassword(string password, string hashedPassword)
  {
    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }
}