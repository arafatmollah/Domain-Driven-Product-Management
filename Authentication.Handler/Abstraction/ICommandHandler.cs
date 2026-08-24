using Authentication.DTO;
using SharedSubsystem.Abstraction.Handlers;

namespace Authentication.Handler.Abstraction;

public interface ICommandHandler<TCommand>
    : SharedSubsystem.Abstraction.Handlers.ICommandHandler<TCommand>
    where TCommand : ICommand
{
}
