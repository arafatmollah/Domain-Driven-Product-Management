using AutoMapper;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class GetProductsHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IQueryHandler<GetProductsQuery, IEnumerable<ProductResponseDto>>
{
    public async Task<IEnumerable<ProductResponseDto>> HandleAsync(
        GetProductsQuery query)
    {
        var products = await productRepository.GetAllAsync();

        return mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }
}