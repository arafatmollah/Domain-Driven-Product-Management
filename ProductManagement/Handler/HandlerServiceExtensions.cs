using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Handler.Abstraction;

namespace ProductManagement.Handler;

public static class HandlerServiceExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        var handlerAssembly = typeof(HandlerServiceExtensions).Assembly;

        var commandHandlerType = typeof(ICommandHandler<,>);
        var queryHandlerType   = typeof(IQueryHandler<,>);

        foreach (var type in handlerAssembly.GetTypes()
                     .Where(t => t is { IsClass: true, IsAbstract: false }))
        {
            foreach (var iface in type.GetInterfaces())
            {
                if (!iface.IsGenericType) continue;

                var definition = iface.GetGenericTypeDefinition();

                if (definition == commandHandlerType || definition == queryHandlerType)
                    services.AddScoped(iface, type);
            }
        }

        return services;
    }
}
