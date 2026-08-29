using SharedSubsystem.Events;

namespace ProductManagement.DTO.Events;

/// <summary>
/// Domain event published by ProductManagement via the Service Bus
/// when a new product is successfully created.
/// OrderManagement (and any other subscriber) reacts to this event.
/// </summary>
public record ProductCreatedEvent : Event
{
    public int ProductId { get; init; }

    public string Name { get; init; } = string.Empty;

    public decimal Price { get; init; }
}
