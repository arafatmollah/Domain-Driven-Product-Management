using SharedSubsystem.Abstraction;

namespace SharedSubsystem.Events;


public abstract record Event : IEvent
{

    public Guid EventId { get; init; } = Guid.NewGuid();


    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;


    public string EventType => GetType().Name;
}
