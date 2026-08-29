using Authentication.Aggregator;
using Authentication.DTO.Command;
using Authentication.Handler.Abstraction;
using Authentication.Repository;
using Authentication.Repository.Context;

namespace Authentication.Handler;

public class RegisterHandler(
    UserAggregatorRoot userAggregatorRoot,
    IUserRepository userRepository,
    AuthDbContext dbContext)
    : ICommandHandler<RegisterCommandDto>
{
    public async Task HandleAsync(
        RegisterCommandDto command,
        CancellationToken cancellationToken = default)
    {
        var existing = await userRepository.GetByEmailAsync(command.Email);
        if (existing is not null)
            throw new InvalidOperationException(
                $"A user with email '{command.Email}' already exists.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

        userAggregatorRoot.Register(command, passwordHash);

        await userRepository.AddAsync(userAggregatorRoot);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to register user.", ex);
        }

        command.UserId = userAggregatorRoot.Id;
    }
}
