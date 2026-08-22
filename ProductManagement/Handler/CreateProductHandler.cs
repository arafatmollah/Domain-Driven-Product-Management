using AutoMapper;
using Aggregator.Services;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Response;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class CreateProductHandler(
    ProductAggregator productAggregator,
    IUnitOfWork uow,
    IMapper mapper)
    : ICommandHandler<CreateProductCommandDto, ProductResponseDto>
{
    public async Task<ProductResponseDto> HandleAsync(
        CreateProductCommandDto command)
    {
        var product = await productAggregator.Create(command);

        var createdProduct =
            await uow.Products.AddAsync(product);

        await uow.SaveChangesAsync();

        return mapper.Map<ProductResponseDto>(createdProduct);
    }
}