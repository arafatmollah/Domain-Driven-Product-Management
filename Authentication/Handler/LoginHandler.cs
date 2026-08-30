using Authentication.DTO.Command;
using Authentication.DTO.Response;
using Authentication.Handler.Services;
using Authentication.Repository;
using Authentication.Repository.Context;
using SharedSubsystem.Abstraction.Handlers;

namespace Authentication.Handler;

public class LoginHandler(
    IUserRepository userRepository,
    IJwtTokenService jwtTokenService,
    AuthDbContext dbContext)
    : SharedSubsystem.Abstraction.Handlers.ICommandHandler<LoginCommandDto>
{
    public async Task HandleAsync(
        LoginCommandDto command,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByEmailAsync(command.Email)
            ?? throw new UnauthorizedAccessException("Invalid email or password.");

        if (!BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var (accessToken, expiresAt) = jwtTokenService.GenerateAccessToken(user);
        var refreshToken = jwtTokenService.GenerateRefreshToken();
        var refreshExpiry = DateTime.UtcNow.AddDays(jwtTokenService.RefreshTokenExpiryDays);

        user.SetRefreshToken(refreshToken, refreshExpiry);
        await userRepository.UpdateAsync(user);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to persist login session.", ex);
        }

        command.Result = new AuthResponseDto
        {
            AccessToken  = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt    = expiresAt,
        };
    }
}
