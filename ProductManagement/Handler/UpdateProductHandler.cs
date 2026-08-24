using Aggregator;
using ProductManagement.DTO.Command;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class UpdateProductHandler(
    IUnitOfWork unitofwork,
    ProductAggregatorRoot productAggregatorRoot)
    : IHandler<UpdateProductCommandDto>
{
    public async Task HandleAsync(UpdateProductCommandDto command)
    {
        var product = await unitofwork.Products.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException(
                $"Product with id {command.Id} was not found.");

        await productAggregatorRoot.Update(command);

        await unitofwork.Products.UpdateAsync(product);

        await unitofwork.SaveChangesAsync();
    }
}