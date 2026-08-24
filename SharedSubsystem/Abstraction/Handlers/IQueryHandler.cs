namespace SharedSubsystem.Abstraction.Handlers;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{

    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
