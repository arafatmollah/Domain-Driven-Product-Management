using Repository.Context;

namespace Repository;

public sealed class UnitOfWork(ProductDbContext context) : IUnitOfWork
{
    private readonly ProductDbContext _context = context;
    private IProductRepository? _products;

    public IProductRepository Products =>
        _products ??= new ProductRepository(_context);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);

    public async ValueTask DisposeAsync()
        => await _context.DisposeAsync();
}
