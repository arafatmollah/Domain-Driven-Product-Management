using Aggregator.Entities;
using ProductManagement.DTO.Filter;

namespace Repository;

public interface IProductRepository : IGenericRepository<Product>
{
    new Task<IEnumerable<Product>> GetAllAsync(IFilter<Product>? filter = null);
}
