using AutoMapper;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using Repository;
using Repository.Filter;
using SharedSubsystem.Abstraction.Handlers;

namespace ProductManagement.Handler;

public class GetProductsHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IQueryHandler<GetProductsQuery, IEnumerable<ProductResponseDto>>
{
    public async Task<IEnumerable<ProductResponseDto>> HandleAsync(
        GetProductsQuery query,
        CancellationToken cancellationToken = default)
    {
        var filter = new ProductFilter
        {
            Search = query.Search,
            MinPrice = query.MinPrice,
            MaxPrice = query.MaxPrice
        };

        var hasFilter = filter.Search is not null
            || filter.MinPrice is not null
            || filter.MaxPrice is not null;

        var products = await productRepository.GetAllAsync(hasFilter ? filter : null);

        return mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }
}