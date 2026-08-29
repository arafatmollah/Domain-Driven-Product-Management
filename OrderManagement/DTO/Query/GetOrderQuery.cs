using OrderManagement.DTO.Response;

namespace OrderManagement.DTO.Query;

public class GetOrderQuery : IQuery<OrderResponseDto>
{
    public int Id { get; set; }
}
