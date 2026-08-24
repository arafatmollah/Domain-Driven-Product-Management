using ProductManagement.DTO;

namespace ProductManagement.Handler.Abstraction;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}
