using Aggregator.Services;
//using ProductManagement.DTO;
using ProductManagement.Handler.Abstraction;
using ProductManagement.DTO.Command;
using ProductManagement.DTO.Response;
using Repository;

namespace ProductManagement.Handler;

public class CreateProductHandler(
    ProductAggregator productAggregator,
    IProductRepository productRepository)
    : ICommandHandler<CreateProductCommandDto, ProductResponseDto>
{
    public async Task<ProductResponseDto> HandleAsync(
        CreateProductCommandDto command)
    {
        var product = productAggregator.Create(command);

        var createdProduct =
            await productRepository.AddAsync(product);

        return new ProductResponseDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price
        };
    }
}
