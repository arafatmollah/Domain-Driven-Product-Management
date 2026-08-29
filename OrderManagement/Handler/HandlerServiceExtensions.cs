using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Handler.Abstraction;
using SharedSubsystem.Abstraction.Handlers;

namespace OrderManagement.Handler;


public static class HandlerServiceExtensions
{

    public static IServiceCollection AddOrderHandlers(
        this IServiceCollection services)
    {
        var assembly = typeof(HandlerServiceExtensions).Assembly;

        var handlers = assembly.GetTypes()
            .Where(IsHandler);

        foreach (var handler in handlers)
        {
            var handlerInterfaces = handler.GetInterfaces()
                .Where(IsHandlerInterface);

            foreach (var iface in handlerInterfaces)
            {
                services.AddScoped(iface, handler);
            }
        }

        return services;
    }

    private static bool IsHandler(Type type)
    {
        return type.IsClass &&
               !type.IsAbstract &&
               type.GetInterfaces().Any(IsHandlerInterface);
    }

    private static bool IsHandlerInterface(Type type)
    {
        if (!type.IsGenericType)
            return false;

        var definition = type.GetGenericTypeDefinition();

        return definition == typeof(SharedSubsystem.Abstraction.Handlers.ICommandHandler<>)
            || definition == typeof(SharedSubsystem.Abstraction.Handlers.IQueryHandler<,>)
            || definition == typeof(IEventHandler<>)
            // Local re-exports used directly by OrderController
            || definition == typeof(OrderManagement.Handler.Abstraction.ICommandHandler<>)
            || definition == typeof(OrderManagement.Handler.Abstraction.IQueryHandler<,>);
    }
}
