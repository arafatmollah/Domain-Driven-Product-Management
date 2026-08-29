using OrderManagement.Aggregator;

namespace OrderManagement.Repository;

public interface IOrderRepository : IGenericRepository<OrderAggregatorRoot>
{
}
