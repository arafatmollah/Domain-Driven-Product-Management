using Aggregator.Services;
using ProductManagement.DTO.Response;
using Repository;

namespace ProductManagement.Handler;

public class CreateProductHandler(
    ProductAggregator productAggregator,
    IProductRepository productRepository)
{
    public async Task<ProductResponseDto> HandleAsync(
        CreateProductRequest request)
    {
        var product = productAggregator.Create(
            request.Name,
            request.Description,
            request.Quantity,
            request.ExpirationDate,
            request.Price);

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