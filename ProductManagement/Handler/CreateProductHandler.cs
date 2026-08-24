using Aggregator;
using AutoMapper;
using ProductManagement.DTO.Command;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class CreateProductHandler(
    ProductAggregatorRoot productAggregatorRoot,
    IUnitOfWork uow)
    : IHandler<CreateProductCommandDto>
{
    public async Task HandleAsync(CreateProductCommandDto command)
    {
        await productAggregatorRoot.Create(command);

        await uow.Products.AddAsync(productAggregatorRoot);

        await uow.SaveChangesAsync();

        command.Id = productAggregatorRoot.Id;
    }
}