using OrderManagement.DTO.Command;

namespace OrderManagement.Aggregator;

public class OrderAggregatorRoot
{
    public int Id { get; private set; }

    public int ProductId { get; private set; }

    public decimal Quantity { get; private set; }

    public string CustomerId { get; private set; } = string.Empty;

    public OrderManagement.DTO.OrderStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }


    private static void Validate(int productId, decimal quantity, string customerId)
    {
        if (productId <= 0)
            throw new ArgumentException("ProductId must be a positive integer.");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("CustomerId is required.");

        if (customerId.Length > 100)
            throw new ArgumentException("CustomerId cannot exceed 100 characters.");
    }


    public void Create(CreateOrderCommandDto command)
    {
        Validate(command.ProductId, command.Quantity, command.CustomerId);

        ProductId  = command.ProductId;
        Quantity   = command.Quantity;
        CustomerId = command.CustomerId;
        Status     = OrderManagement.DTO.OrderStatus.Pending;
        CreatedAt  = DateTime.UtcNow;
    }

    public void CreateFromEvent(int productId, decimal quantity, string customerId)
    {
        Validate(productId, quantity, customerId);

        ProductId  = productId;
        Quantity   = quantity;
        CustomerId = customerId;
        Status     = OrderManagement.DTO.OrderStatus.Pending;
        CreatedAt  = DateTime.UtcNow;
    }

    public void Update(UpdateOrderCommandDto command)
    {
        Validate(command.ProductId, command.Quantity, command.CustomerId);

        ProductId  = command.ProductId;
        Quantity   = command.Quantity;
        CustomerId = command.CustomerId;
        Status     = command.Status;
    }
}
