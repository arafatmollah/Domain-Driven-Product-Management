using ProductManagement.DTO;

namespace ProductManagement.Handler.Abstraction
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
