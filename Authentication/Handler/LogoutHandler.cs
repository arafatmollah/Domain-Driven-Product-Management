using Authentication.DTO.Command;
using Authentication.Repository;
using Authentication.Repository.Context;
using SharedSubsystem.Abstraction.Handlers;

namespace Authentication.Handler;

public class LogoutHandler(
    IUserRepository userRepository,
    AuthDbContext dbContext)
    : ICommandHandler<LogoutCommandDto>
{
    public async Task HandleAsync(
        LogoutCommandDto command,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(command.UserId)
            ?? throw new KeyNotFoundException(
                $"User with id '{command.UserId}' was not found.");

        user.ClearRefreshToken();
        await userRepository.UpdateAsync(user);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to invalidate session.", ex);
        }
    }
}
