using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Handler.Abstraction;

namespace ProductManagement.Handler;

public static class HandlerServiceExtensions
{
    public static IServiceCollection AddHandlers(
        this IServiceCollection services)
    {
        var assembly = typeof(HandlerServiceExtensions).Assembly;

        var handlers = assembly.GetTypes()
            .Where(IsHandler);

        foreach (var handler in handlers)
        {
            var handlerInterface = handler.GetInterfaces()
                .First(IsHandlerInterface);

            services.AddScoped(handlerInterface, handler);
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

        return definition == typeof(ICommandHandler<>) ||
               definition == typeof(IQueryHandler<,>);
    }
}