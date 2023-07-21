using AutoMapper;
using backend.API.DTOs;
using backend.Core.Interfaces.Services;
using backend.Core.Models;
using backend.Persistence;
using MongoDB.Driver;

namespace backend.Application.Services;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> _usersCollection;
    private readonly IMapper _mapper;

    public UserService(MongoDbContext dbContext, IMapper mapper)
    {
        _usersCollection = dbContext.Users;
        _mapper = mapper;
    }
        
    public async Task<bool> IsUsernameTaken(string username)
    {
        var existingUser = await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
        return existingUser != null;
    }

    public async Task<bool> RegisterUser(RegisterDto registerDto)
    {
        try
        {
            if (await IsUsernameTaken(registerDto.Username))
            {
                return false; // Username already exists
            }

            var user = new User
            {
                Username = registerDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };

            await _usersCollection.InsertOneAsync(user);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}