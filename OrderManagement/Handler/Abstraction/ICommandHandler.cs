using SharedSubsystem.Abstraction;

namespace OrderManagement.Handler.Abstraction;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
