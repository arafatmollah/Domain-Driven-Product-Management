using OrderManagement.DTO.Command;
using OrderManagement.Repository;
using OrderManagement.Repository.Context;
using SharedSubsystem.Abstraction.Handlers;

namespace OrderManagement.Handler;

public class DeleteOrderHandler(
    IOrderRepository orderRepository,
    OrderDbContext dbContext)
    : ICommandHandler<DeleteOrderCommandDto>
{
    public async Task HandleAsync(
        DeleteOrderCommandDto command,
        CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException(
                $"Order with Id '{command.Id}' was not found.");

        await orderRepository.DeleteAsync(order);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to delete order.", ex);
        }
    }
}
