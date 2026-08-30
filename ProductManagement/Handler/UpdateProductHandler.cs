using Aggregator;
using ProductManagement.DTO.Command;
using Repository;
using Repository.Context;
using SharedSubsystem.Abstraction.Handlers;

namespace ProductManagement.Handler;

public class UpdateProductHandler(
    IProductRepository productRepository,
    ProductDbContext dbContext,
    ProductAggregatorRoot productAggregatorRoot)
    : SharedSubsystem.Abstraction.Handlers.ICommandHandler<UpdateProductCommandDto>
{
    public async Task HandleAsync(
        UpdateProductCommandDto command,
        CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException(
                $"Product with id {command.Id} was not found.");

        productAggregatorRoot.Update(command);

        await productRepository.UpdateAsync(product);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}