using ProductManagement.DTO;

namespace ProductManagement.Handler.Abstraction
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
}
}
