using System.Text;
using System.Security.Claims;
using Backend.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Services;

public class TokenService : ITokenService
{
  private readonly IConfiguration _config;
  private readonly SymmetricSecurityKey _key;
  private readonly UserManager<User> _userManager;

  public TokenService(IConfiguration config, UserManager<User> userManager)
  {
    _config = config;
    _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:SecretKey"]));
    _userManager = userManager;
  }

  public async Task<User> GetUserFromValidToken(string token)
  {
    var tokenHandler = new JwtSecurityTokenHandler();

    try
    {
      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = _key,
        ValidateIssuer = true,
        ValidIssuer = _config["JwtConfig:Issuer"],
        ValidateAudience = true,
        ValidAudience = _config["JwtConfig:Audience"],
        ClockSkew = TimeSpan.Zero
      }, out SecurityToken validatedToken);

      if (validatedToken is JwtSecurityToken jwtToken)
      {
        var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
        if (!string.IsNullOrEmpty(emailClaim))
        {
          var user = _userManager.FindByEmailAsync(emailClaim).Result;
          return user;
        }
      }
    }
    catch (Exception ex)
    {
      throw new Exception("Invalid token", ex);
    }

    return null;
  }

  public string GenerateJwtToken(User user)
  {
    var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>
    {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
    };

    var token = new JwtSecurityToken(
        issuer: _config["JwtConfig:Issuer"],
        audience: _config["JwtConfig:Audience"],
        claims,
        expires: DateTime.UtcNow.AddMinutes(30),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
