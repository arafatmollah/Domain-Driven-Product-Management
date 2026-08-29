using Authentication.Aggregator;
using Authentication.DTO.Response;

namespace Authentication.Handler.Services;

public interface IJwtTokenService
{

    (string Token, DateTime ExpiresAt) GenerateAccessToken(UserAggregatorRoot user);


    string GenerateRefreshToken();


    int AccessTokenExpiryMinutes { get; }

    int RefreshTokenExpiryDays { get; }
}
