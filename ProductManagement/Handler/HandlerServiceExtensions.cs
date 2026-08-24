using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Handler.Abstraction;
using SharedSubsystem.Abstraction.Handlers;

namespace ProductManagement.Handler;

/// <summary>
/// Extension methods for registering all ProductManagement handlers with DI.
/// </summary>
public static class HandlerServiceExtensions
{
    /// <summary>
    /// Scans the ProductManagement.Handler assembly for all command and query
    /// handler implementations and registers them as scoped services.
    /// </summary>
    public static IServiceCollection AddHandlers(
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

        // Match both the shared base interfaces and the local re-exports
        return definition == typeof(SharedSubsystem.Abstraction.Handlers.ICommandHandler<>)
            || definition == typeof(SharedSubsystem.Abstraction.Handlers.IQueryHandler<,>)
            || definition == typeof(IEventHandler<>)
            // Local re-exports used directly by ProductController
            || definition == typeof(ProductManagement.Handler.Abstraction.ICommandHandler<>)
            || definition == typeof(ProductManagement.Handler.Abstraction.IQueryHandler<,>);
    }
}