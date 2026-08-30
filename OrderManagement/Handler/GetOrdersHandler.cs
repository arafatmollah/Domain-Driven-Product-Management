using AutoMapper;
using OrderManagement.DTO.Query;
using OrderManagement.DTO.Response;
using OrderManagement.Repository;
using OrderManagement.Repository.Filter;
using SharedSubsystem.Abstraction.Handlers;

namespace OrderManagement.Handler;

public class GetOrdersHandler(
    IOrderRepository orderRepository,
    IMapper mapper)
    : IQueryHandler<GetOrdersQuery, IEnumerable<OrderResponseDto>>
{
    public async Task<IEnumerable<OrderResponseDto>> HandleAsync(
        GetOrdersQuery query,
        CancellationToken cancellationToken = default)
    {
        var filter = new OrderFilter
        {
            CustomerId = query.CustomerId,
            Status     = query.Status
        };

        var hasFilter = filter.CustomerId is not null
            || filter.Status is not null;

        var orders = await orderRepository.GetAllAsync(hasFilter ? filter : null);

        return mapper.Map<IEnumerable<OrderResponseDto>>(orders);
    }
}
