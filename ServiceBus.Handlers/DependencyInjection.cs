using Microsoft.Extensions.DependencyInjection;

namespace ServiceBus.Handlers;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceBus(
        this IServiceCollection services)
    {
        services.AddScoped<IServiceBus, ServiceBus>();

        return services;
    }
}