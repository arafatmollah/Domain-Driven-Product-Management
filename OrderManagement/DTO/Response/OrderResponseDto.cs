namespace OrderManagement.DTO.Response;

public class OrderResponseDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public string CustomerId { get; set; } = string.Empty;

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}
