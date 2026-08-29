using Authentication.DTO.Command;
using Authentication.DTO.Response;
using Authentication.Handler.Abstraction;
using Authentication.Handler.Services;
using Authentication.Repository;
using Authentication.Repository.Context;

namespace Authentication.Handler;

public class RefreshTokenHandler(
    IUserRepository userRepository,
    IJwtTokenService jwtTokenService,
    AuthDbContext dbContext)
    : ICommandHandler<RefreshTokenCommandDto>
{
    public async Task HandleAsync(
        RefreshTokenCommandDto command,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByRefreshTokenAsync(command.RefreshToken)
            ?? throw new UnauthorizedAccessException("Refresh token is invalid.");

        if (user.RefreshTokenExpiry is null || user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token has expired.");

        var (accessToken, expiresAt) = jwtTokenService.GenerateAccessToken(user);
        var newRefreshToken = jwtTokenService.GenerateRefreshToken();
        var refreshExpiry = DateTime.UtcNow.AddDays(jwtTokenService.RefreshTokenExpiryDays);

        user.SetRefreshToken(newRefreshToken, refreshExpiry);
        await userRepository.UpdateAsync(user);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to persist refreshed session.", ex);
        }

        command.Result = new AuthResponseDto
        {
            AccessToken  = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt    = expiresAt,
        };
    }
}
