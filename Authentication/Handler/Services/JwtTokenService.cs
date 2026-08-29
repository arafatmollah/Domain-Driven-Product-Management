using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Authentication.Aggregator;
using Authentication.DTO.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Handler.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly string _secret;
    private readonly string _issuer;
    private readonly string _audience;

    public int AccessTokenExpiryMinutes { get; }
    public int RefreshTokenExpiryDays { get; }

    public JwtTokenService(IConfiguration configuration)
    {
        var section = configuration.GetSection("Jwt");

        _secret   = section["Secret"]   ?? throw new InvalidOperationException("Jwt:Secret is not configured.");
        _issuer   = section["Issuer"]   ?? throw new InvalidOperationException("Jwt:Issuer is not configured.");
        _audience = section["Audience"] ?? throw new InvalidOperationException("Jwt:Audience is not configured.");

        AccessTokenExpiryMinutes = int.TryParse(section["AccessTokenExpiryMinutes"], out var min)  ? min  : 60;
        RefreshTokenExpiryDays   = int.TryParse(section["RefreshTokenExpiryDays"],   out var days) ? days : 7;
    }

    public (string Token, DateTime ExpiresAt) GenerateAccessToken(UserAggregatorRoot user)
    {
        var key   = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresAt = DateTime.UtcNow.AddMinutes(AccessTokenExpiryMinutes);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,   user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer:             _issuer,
            audience:           _audience,
            claims:             claims,
            expires:            expiresAt,
            signingCredentials: creds);

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }

    public string GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}
