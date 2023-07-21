using backend.Core.Models;
using backend.API.DTOs;
using MongoDB.Driver;
using backend.Core.Interfaces.Services;
using backend.Persistence;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Application.Services;

public class UserService : IUserService
{
  private readonly IMongoCollection<User> _usersCollection;
  private readonly IConfiguration _configuration;

  public UserService(MongoDbContext dbContext, IConfiguration configuration)
  {
    _usersCollection = dbContext.Users;
    _configuration = configuration;
  }

  public async Task<User> GetUserByUsername(string username)
  {
    return await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
  }

  public async Task<bool> RegisterUser(RegisterDto registerDto)
  {
    var existingUser = await GetUserByUsername(registerDto.Username);
    if (existingUser != null)
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

  public async Task<string?> Login(LoginDto loginDto)
  {
    var user = await GetUserByUsername(loginDto.Username);
    if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
    {
      return null;
    }

    return GenerateJwtToken(user);
  }

  private string HashPassword(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(password);
  }

  private bool VerifyPassword(string password, string hashedPassword)
  {
    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }

  private string GenerateJwtToken(User user)
  {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SecretKey"]));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
            };

    var token = new JwtSecurityToken(
        _configuration["JwtConfig:Issuer"],
        _configuration["JwtConfig:Audience"],
        claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
