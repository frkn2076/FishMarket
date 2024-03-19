using FishMarket.Api.Models.DTOs;
using FishMarket.Api.Services.Contracts;
using FishMarket.Api.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FishMarket.Api.Services.Implementations;

public class AuthenticationService(IOptions<JWTSettings> jwtSettings) : IAuthenticationService
{
    public AuthenticationModel GenerateToken(string email)
    {
        return new()
        {
            AccessToken = GenerateJWTToken(email, false),
            AccessTokenExpiresInHours = jwtSettings.Value.AccessTokenExpiresInHour,
            RefreshToken = GenerateJWTToken(email, true),
            RefreshTokenExpiresInHours = jwtSettings.Value.RefreshTokenExpiresInHour
        };
    }

    #region Helper

    private string GenerateJWTToken(string email, bool isRefreshToken)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.AuthenticationMethod, isRefreshToken.ToString())
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Value.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(isRefreshToken ? jwtSettings.Value.RefreshTokenExpiresInHour : jwtSettings.Value.AccessTokenExpiresInHour),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    #endregion
}
