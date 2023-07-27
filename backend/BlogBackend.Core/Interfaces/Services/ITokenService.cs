using BlogBackend.Core.Models;

namespace BlogBackend.Core.Interfaces.Services;

public interface ITokenService
{
  string GenerateJwtToken(User user);
}