using Aggregator;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Events;
using ProductManagement.Handler.Abstraction;
using Repository;
using Repository.Context;
using ServiceBus.Handlers;

namespace ProductManagement.Handler;

public class CreateProductHandler(
    ProductAggregatorRoot productAggregatorRoot,
    IProductRepository productRepository,
    ProductDbContext dbContext,
    IServiceBus serviceBus)
    : ICommandHandler<CreateProductCommandDto>
{
    public async Task HandleAsync(
        CreateProductCommandDto command,
        CancellationToken cancellationToken = default)
    {
        productAggregatorRoot.Create(command);

        await productRepository.AddAsync(productAggregatorRoot);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to save product.", ex);
        }

        command.Id = productAggregatorRoot.Id;

        // Publish event so other bounded contexts (e.g. OrderManagement)
        // can react via the Service Bus.
        await serviceBus.PublishEventAsync(
            new ProductCreatedEvent
            {
                ProductId = productAggregatorRoot.Id,
                Name      = productAggregatorRoot.Name,
                Price     = productAggregatorRoot.Price
            },
            cancellationToken);
    }
}