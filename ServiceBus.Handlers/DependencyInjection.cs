using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBus.Handlers.RabbitMQ;

namespace ServiceBus.Handlers;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceBus(
        this IServiceCollection services)
    {
        services.AddScoped<IServiceBus, ServiceBus>();

        services.AddSingleton<IRabbitMqPublisher>(sp =>
        {
            var configuration =
                sp.GetRequiredService<IConfiguration>();

            var options = configuration
                .GetSection("RabbitMQ")
                .Get<RabbitMqOptions>()
                ?? throw new InvalidOperationException(
                    "RabbitMQ configuration is missing.");

            return new RabbitMqPublisher(options);
        });

        return services;
    }
}