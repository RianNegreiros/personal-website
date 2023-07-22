using backend.API.DTOs;
using backend.Core.Models;

namespace backend.Core.Interfaces.Services;

public interface IUserService
{
  Task<User> GetCurrentUser(string userEmail);
  Task<bool> CheckEmailExists(string email);
}