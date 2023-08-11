using Backend.Core.Models;

namespace Backend.Application.Services;

public interface ITokenService
{
  string GenerateJwtToken(User user, bool isAdmin = false);
}