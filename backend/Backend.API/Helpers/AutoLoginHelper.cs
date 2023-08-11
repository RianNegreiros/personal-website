using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Backend.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.API.Helpers;

public static class AutoLoginHelper
{
    public static string GetEmailFromValidToken(IConfiguration config, string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtConfig:SecretKey"]));
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = config["JwtConfig:Issuer"],
                ValidateAudience = true,
                ValidAudience = config["JwtConfig:Audience"],
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            if (validatedToken is JwtSecurityToken jwtToken)
            {
                var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
                return emailClaim;
            }
        }
        catch (Exception)
        {
            // Invalid token or other exception handling
        }

        return null;
    }

    // Add a method to retrieve configuration values if needed
    public static string GetJwtConfigValue(IConfiguration config, string key)
    {
        return config["JwtConfig:" + key];
    }
}