using Aggregator;
using ProductManagement.DTO.Command;
using ProductManagement.Handler.Abstraction;
using Repository;
using Repository.Context;

namespace ProductManagement.Handler;

public class CreateProductHandler(
    ProductAggregatorRoot productAggregatorRoot,
    IProductRepository productRepository,
    ProductDbContext dbContext)
    : ICommandHandler<CreateProductCommandDto>
{
    public async Task HandleAsync(CreateProductCommandDto command)
    {
        productAggregatorRoot.Create(command);

        await productRepository.AddAsync(productAggregatorRoot);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to save product.", ex);
        }
        command.Id = productAggregatorRoot.Id;
    }
}