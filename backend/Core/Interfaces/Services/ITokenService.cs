using backend.Core.Models;

namespace backend.Core.Interfaces.Services;

public interface ITokenService
{
  string GenerateJwtToken(User user);
}