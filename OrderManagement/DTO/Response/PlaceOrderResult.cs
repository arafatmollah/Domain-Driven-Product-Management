namespace OrderManagement.DTO.Response;


public class PlaceOrderResult
{

    public int OrderId { get; set; }


    public string CustomerId { get; set; } = string.Empty;

    public decimal Total { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}
