using Aggregator;
using AutoMapper;
using ProductManagement.DTO.Command;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class CreateProductHandler(
    ProductAggregatorRoot productAggregatorRoot,
    IUnitOfWork uow)
    : ICommandHandler<CreateProductCommandDto>
{
    public async Task HandleAsync(CreateProductCommandDto command)
    {
        productAggregatorRoot.Create(command);

        await uow.Products.AddAsync(productAggregatorRoot);

        try
        {
            await uow.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to save product.", ex);
        }
        command.Id = productAggregatorRoot.Id;
    }
}