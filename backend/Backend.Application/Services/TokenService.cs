using System.Text;
using System.Security.Claims;
using Backend.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Backend.Application.Services;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;
  private readonly SymmetricSecurityKey _key;

  public TokenService(IConfiguration config)
  {
    _config = config;
    _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JwtConfig:SecretKey"]));
  }

  public string GenerateJwtToken(User user, bool isAdmin = false)
  {
    SigningCredentials credentials = new(_key, SecurityAlgorithms.HmacSha256Signature);

    JwtSecurityTokenHandler handler = new();

    SecurityTokenDescriptor tokenDescriptor = new()
    {
      Issuer = _config["JwtConfig:Issuer"],
      Audience = _config["JwtConfig:Audience"],
      SigningCredentials = credentials,
      Expires = DateTime.UtcNow.AddMinutes(30),
      NotBefore = DateTime.UtcNow,
      IssuedAt = DateTime.UtcNow,
      Subject = GenerateClaims(user, isAdmin)
    };

    var token = handler.CreateToken(tokenDescriptor);

    return handler.WriteToken(token);
  }

  private static ClaimsIdentity GenerateClaims(User user, bool isAdmin = false)
  {
    var ci = new ClaimsIdentity();
    ci.AddClaim(
      new Claim(ClaimTypes.Email, user.Email)
    );

    ci.AddClaim(
      new Claim(ClaimTypes.Name, user.UserName)
    );

    ci.AddClaim(
      new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
    );

    return ci;
  }
}
