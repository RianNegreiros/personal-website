using backend.API.DTOs;
using backend.Core.Models;

namespace backend.Core.Interfaces.Services;

public interface IUserService
{
  Task<User> GetUserByUsername(string username);
  Task<bool> RegisterUser(RegisterDto registerDto);
  Task<string?> Login(LoginDto loginDto);
}