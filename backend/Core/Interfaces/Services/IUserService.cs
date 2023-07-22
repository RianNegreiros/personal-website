using backend.API.DTOs;

namespace backend.Core.Interfaces.Services;

public interface IUserService
{
  Task<UserDto> GetCurrentUser(string userEmail);
  Task<bool> CheckEmailExists(string email);
}