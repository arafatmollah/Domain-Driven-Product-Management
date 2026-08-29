using Microsoft.Extensions.DependencyInjection;
using ServiceBus.Handlers.RabbitMQ;
using SharedSubsystem.Abstraction;
using SharedSubsystem.Abstraction.Handlers;

namespace ServiceBus.Handlers;

public sealed class ServiceBus : IServiceBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRabbitMqPublisher _rabbitMqPublisher;

    public ServiceBus(
        IServiceProvider serviceProvider,
        IRabbitMqPublisher rabbitMqPublisher)
    {
        _serviceProvider = serviceProvider;
        _rabbitMqPublisher = rabbitMqPublisher;
    }

    public async Task SendCommandAsync<TCommand>(
        TCommand command,
        CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService<ICommandHandler<TCommand>>()
            ?? throw new InvalidOperationException(
                $"No handler registered for command '{typeof(TCommand).Name}'. " +
                $"Ensure an ICommandHandler<{typeof(TCommand).Name}> is registered in DI.");

        await handler.HandleAsync(command, cancellationToken);
    }

    public async Task<TResult> SendQueryAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResult>
    {
        var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>()
            ?? throw new InvalidOperationException(
                $"No handler registered for query '{typeof(TQuery).Name}'. " +
                $"Ensure an IQueryHandler<{typeof(TQuery).Name}, {typeof(TResult).Name}> is registered in DI.");

        return await handler.HandleAsync(query, cancellationToken);
    }

    public async Task PublishEventAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : IEvent
    {
        await _rabbitMqPublisher.PublishAsync(
            @event,
            cancellationToken);
    }
}