using ProductManagement.DTO;
using ProductManagement.DTO.Query;
using ProductManagement.Handler.Abstraction;
using ProductManagement.DTO.Response;
using Repository;

namespace ProductManagement.Handler;

public class GetProductByIdHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductQuery, ProductResponseDto>
{
    public async Task<ProductResponseDto> HandleAsync(GetProductQuery query)
    {
        var product = await productRepository.GetByIdAsync(query.Id)
            ?? throw new KeyNotFoundException($"Product with id {query.Id} was not found.");

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
    }
}

