using Aggregator;
using ProductManagement.DTO.Filter;

namespace Repository;

public interface IProductRepository : IGenericRepository<ProductAggregatorRoot>
{
    new Task<IEnumerable<ProductAggregatorRoot>> GetAllAsync(IFilter<ProductAggregatorRoot>? filter = null);
}
