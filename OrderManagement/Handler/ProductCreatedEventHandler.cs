using OrderManagement.Aggregator;
using OrderManagement.DTO.Events;
using OrderManagement.Repository;
using OrderManagement.Repository.Context;
using SharedSubsystem.Abstraction.Handlers;

namespace OrderManagement.Handler;

public class ProductCreatedEventHandler(
    IOrderRepository orderRepository,
    OrderDbContext dbContext)
    : IEventHandler<ProductCreatedIntegrationEvent>
{
    public async Task HandleAsync(
        ProductCreatedIntegrationEvent @event,
        CancellationToken cancellationToken = default)
    {
        var order = new OrderAggregatorRoot();

        order.CreateFromEvent(
            productId:  @event.ProductId,
            quantity:   1,
            customerId: "system");

        await orderRepository.AddAsync(order);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"Failed to auto-create order for ProductId '{@event.ProductId}'.", ex);
        }
    }
}
