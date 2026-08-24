using SharedSubsystem.Abstraction;
using SharedSubsystem.Abstraction.Handlers;

namespace ProductManagement.Handler.Abstraction;

/// <summary>
/// ProductManagement-scoped command handler contract.
/// Re-exports <see cref="ICommandHandler{TCommand}"/> from SharedSubsystem.
/// </summary>
public interface ICommandHandler<TCommand>
    : SharedSubsystem.Abstraction.Handlers.ICommandHandler<TCommand>
    where TCommand : ICommand
{
}
