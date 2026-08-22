using AutoMapper;
using ProductManagement.DTO.Query;
using ProductManagement.DTO.Response;
using ProductManagement.Handler.Abstraction;
using Repository;

namespace ProductManagement.Handler;

public class GetProductByIdHandler(
    IUnitOfWork uow,
    IMapper mapper)
    : IQueryHandler<GetProductQuery, ProductResponseDto>
{
    public async Task<ProductResponseDto> HandleAsync(
        GetProductQuery query)
    {
        var product = await uow.Products.GetByIdAsync(query.Id)
            ?? throw new KeyNotFoundException(
                $"Product with id {query.Id} was not found.");

        return mapper.Map<ProductResponseDto>(product);
    }
}