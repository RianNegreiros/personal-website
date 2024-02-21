using Backend.Core.Models;

namespace Backend.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(User user, DateTime expiresTime, bool isAdmin = false);
}