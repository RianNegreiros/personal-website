using BlogBackend.Core.Models;

namespace BlogBackend.Application.Services;

public interface ITokenService
{
  string GenerateJwtToken(User user);
}