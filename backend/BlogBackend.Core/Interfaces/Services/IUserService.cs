using BlogBackend.Core.Models;

namespace BlogBackend.Core.Interfaces.Services;

public interface IUserService
{
  Task<User> GetCurrentUser(string userEmail);
  Task<bool> CheckEmailExists(string email);
}