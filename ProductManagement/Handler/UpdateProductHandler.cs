using Aggregator.Services;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Response;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class UpdateProductHandler(
    ProductAggregator productAggregator,
    IProductRepository productRepository)
    : ICommandHandler<UpdateProductCommandDto, ProductResponseDto>
{
    public async Task<ProductResponseDto> HandleAsync(UpdateProductCommandDto command)
    {
        var product = await productRepository.GetByIdAsync(command.Id)
            ?? throw new KeyNotFoundException($"Product with id {command.Id} was not found.");

        productAggregator.Update(product, command);

        await productRepository.UpdateAsync(product);

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Quantity = product.Quantity,
            Price = product.Price
        };
    }
}
