using Aggregator;
using AutoMapper;
using ProductManagement.DTO.Response;

namespace ProductManagement.Handler.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductAggregatorRoot, ProductResponseDto>();
    }
}