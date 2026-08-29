using AutoMapper;
using OrderManagement.DTO.Query;
using OrderManagement.DTO.Response;
using OrderManagement.Handler.Abstraction;
using OrderManagement.Repository;

namespace OrderManagement.Handler;

public class GetOrderByIdHandler(
    IOrderRepository orderRepository,
    IMapper mapper)
    : IQueryHandler<GetOrderQuery, OrderResponseDto>
{
    public async Task<OrderResponseDto> HandleAsync(
        GetOrderQuery query,
        CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(query.Id)
            ?? throw new KeyNotFoundException(
                $"Order with Id '{query.Id}' was not found.");

        return mapper.Map<OrderResponseDto>(order);
    }
}
