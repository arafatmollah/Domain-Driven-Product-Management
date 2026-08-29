using SharedSubsystem.Events;

namespace OrderManagement.DTO.Events;

public record ProductCreatedIntegrationEvent : Event
{
    public int ProductId { get; init; }

    public string ProductName { get; init; } = string.Empty;

    public decimal Price { get; init; }
}
