using ProductManagement.DTO;

namespace ProductManagement.Handler.Abstraction;

public interface IHandler<TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}
