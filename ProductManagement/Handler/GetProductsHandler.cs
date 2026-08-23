using AutoMapper;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using ProductManagement.Handler.Abstraction;
using Repository;
using Repository.Filter;

namespace ProductManagement.Handler;

public class GetProductsHandler(
    IUnitOfWork unitofwork,
    IMapper mapper)
    : IQueryHandler<GetProductsQuery, IEnumerable<ProductResponseDto>>
{
    public async Task<IEnumerable<ProductResponseDto>> HandleAsync(
        GetProductsQuery query)
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

        var products = await unitofwork.Products.GetAllAsync(hasFilter ? filter : null);

        return mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }
}