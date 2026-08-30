using AutoMapper;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using Repository;
using SharedSubsystem.Abstraction.Handlers;

namespace ProductManagement.Handler;

public class GetProductByIdHandler(
    IProductRepository productRepository,
    IMapper mapper)
    : IQueryHandler<GetProductQuery, ProductResponseDto>
{
    public async Task<ProductResponseDto> HandleAsync(
        GetProductQuery query,
        CancellationToken cancellationToken = default)
    {
        var product = await productRepository.GetByIdAsync(query.Id)
            ?? throw new KeyNotFoundException(
                $"Product with id {query.Id} was not found.");

        return mapper.Map<ProductResponseDto>(product);
    }
}
