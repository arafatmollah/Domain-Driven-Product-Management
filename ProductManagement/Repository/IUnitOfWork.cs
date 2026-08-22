namespace Repository;

public interface IUnitOfWork : IAsyncDisposable
{
    IProductRepository Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
