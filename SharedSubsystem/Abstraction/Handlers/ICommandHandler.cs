namespace SharedSubsystem.Abstraction.Handlers;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{

    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
