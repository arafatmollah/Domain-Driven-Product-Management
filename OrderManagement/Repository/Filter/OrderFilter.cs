using OrderManagement.Aggregator;
using OrderManagement.DTO.Filter;

namespace OrderManagement.Repository.Filter;

public class OrderFilter : IFilter<OrderAggregatorRoot>
{
    public string? CustomerId { get; set; }

    public OrderManagement.DTO.OrderStatus? Status { get; set; }

    public IQueryable<OrderAggregatorRoot> Apply(
        IQueryable<OrderAggregatorRoot> query)
    {
        if (!string.IsNullOrWhiteSpace(CustomerId))
        {
            query = query.Where(x => x.CustomerId == CustomerId);
        }

        if (Status.HasValue)
        {
            query = query.Where(x => x.Status == Status.Value);
        }

        return query;
    }
}
