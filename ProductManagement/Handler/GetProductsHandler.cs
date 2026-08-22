using ProductManagement.DTO.Query;
using ProductManagement.Handler.Abstraction;
using ProductManagement.DTO.Response;
using Repository;

namespace ProductManagement.Handler;

public class GetProductsHandler(IProductRepository productRepository)
    : IQueryHandler<GetProductsQuery, IEnumerable<ProductResponseDto>>
{
    public async Task<IEnumerable<ProductResponseDto>> HandleAsync(GetProductsQuery query)
    {
        var products = await productRepository.GetAllAsync();

        return products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Quantity = p.Quantity,
            Price = p.Price
        });
    }
}
