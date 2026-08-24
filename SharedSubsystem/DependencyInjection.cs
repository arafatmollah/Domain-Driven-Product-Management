using Microsoft.Extensions.DependencyInjection;
using SharedSubsystem.Abstraction.Handlers;
using System.Reflection;

namespace SharedSubsystem;


public static class DependencyInjection
{

    public static IServiceCollection AddSharedSubsystem(
        this IServiceCollection services,
        Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract);

        foreach (var handlerType in handlerTypes)
        {
            var interfaces = handlerType.GetInterfaces()
                .Where(IsHandlerInterface);

            foreach (var iface in interfaces)
            {
                services.AddScoped(iface, handlerType);
            }
        }

        return services;
    }

    private static bool IsHandlerInterface(Type type)
    {
        if (!type.IsGenericType)
            return false;

        var def = type.GetGenericTypeDefinition();

        return def == typeof(ICommandHandler<>)
            || def == typeof(IQueryHandler<,>)
            || def == typeof(IEventHandler<>);
    }
}
