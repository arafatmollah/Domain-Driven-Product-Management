using OrderManagement.DTO.Response;

namespace OrderManagement.DTO.Query;

public class GetOrdersQuery : IQuery<IEnumerable<OrderResponseDto>>
{
    public string? CustomerId { get; set; }

    public OrderStatus? Status { get; set; }
}
