using Authentication.DTO;
using SharedSubsystem.Abstraction.Handlers;

namespace Authentication.Handler.Abstraction;

public interface IQueryHandler<TQuery, TResult>
    : SharedSubsystem.Abstraction.Handlers.IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}
