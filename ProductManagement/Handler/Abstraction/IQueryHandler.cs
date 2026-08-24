using SharedSubsystem.Abstraction;
using SharedSubsystem.Abstraction.Handlers;

namespace ProductManagement.Handler.Abstraction;

/// <summary>
/// ProductManagement-scoped query handler contract.
/// Re-exports <see cref="IQueryHandler{TQuery,TResult}"/> from SharedSubsystem.
/// </summary>
public interface IQueryHandler<TQuery, TResult>
    : SharedSubsystem.Abstraction.Handlers.IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}
