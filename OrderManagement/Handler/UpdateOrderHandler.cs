using OrderManagement.DTO.Command;
using OrderManagement.Handler.Abstraction;
using OrderManagement.Repository;
using OrderManagement.Repository.Context;

namespace OrderManagement.Handler;

public class UpdateOrderHandler(
    IOrderRepository orderRepository,
    OrderDbContext dbContext)
    : ICommandHandler<UpdateOrderCommandDto>
{
    public async Task HandleAsync(
        UpdateOrderCommandDto command,
        CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException(
                $"Order with Id '{command.Id}' was not found.");

        order.Update(command);

        await orderRepository.UpdateAsync(order);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to update order.", ex);
        }
    }
}
