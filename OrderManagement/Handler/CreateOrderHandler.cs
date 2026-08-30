using OrderManagement.Aggregator;
using OrderManagement.DTO.Command;
using OrderManagement.Repository;
using OrderManagement.Repository.Context;
using SharedSubsystem.Abstraction.Handlers;

namespace OrderManagement.Handler;

public class CreateOrderHandler(
    OrderAggregatorRoot orderAggregatorRoot,
    IOrderRepository orderRepository,
    OrderDbContext dbContext)
    : ICommandHandler<CreateOrderCommandDto>
{
    public async Task HandleAsync(
        CreateOrderCommandDto command,
        CancellationToken cancellationToken = default)
    {
        orderAggregatorRoot.Create(command);

        await orderRepository.AddAsync(orderAggregatorRoot);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to save order.", ex);
        }

        command.Id = orderAggregatorRoot.Id;
    }
}
