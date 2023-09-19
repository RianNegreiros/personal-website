using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Backend.API.Helpers;

public static class AutoLoginHelper
{
    public static string? GetEmailFromValidToken(IConfiguration config, string token)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(config["JwtConfig:SecretKey"]));
        JwtSecurityTokenHandler tokenHandler = new();

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
                string? emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
                return emailClaim;
            }
        }
        catch (Exception)
        {
            throw new SecurityTokenException("Invalid token");
        }

        return null;
    }

    public static string GetJwtConfigValue(IConfiguration config, string key)
    {
        return config["JwtConfig:" + key];
    }
}