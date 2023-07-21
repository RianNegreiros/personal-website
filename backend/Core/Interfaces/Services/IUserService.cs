using backend.API.DTOs;

namespace backend.Core.Interfaces.Services;

public interface IUserService
{
    Task<bool> IsUsernameTaken(string username);
    Task<bool> RegisterUser(RegisterDto registerDto);
}