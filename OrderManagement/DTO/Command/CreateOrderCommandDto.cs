namespace OrderManagement.DTO.Command;

public class CreateOrderCommandDto : ICommand
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();

    public int Id { get; set; }

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public string CustomerId { get; set; } = string.Empty;
}
