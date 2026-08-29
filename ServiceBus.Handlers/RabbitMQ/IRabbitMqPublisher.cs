using SharedSubsystem.Abstraction;

namespace ServiceBus.Handlers.RabbitMQ;

public interface IRabbitMqPublisher
{
    Task PublishAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : IEvent;
}