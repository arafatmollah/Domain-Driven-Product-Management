namespace SharedSubsystem.Abstraction;


public interface IEvent
{
    Guid EventId { get; }

    DateTime OccurredOn { get; }

    string EventType { get; }
}
