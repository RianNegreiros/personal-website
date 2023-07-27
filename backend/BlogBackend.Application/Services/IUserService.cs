using BlogBackend.Core.Models;

namespace BlogBackend.Application.Services;

public interface IUserService
{
  Task<User> GetCurrentUser(string userEmail);
  Task<bool> CheckEmailExists(string email);
}