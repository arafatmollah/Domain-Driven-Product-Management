using AutoMapper;
using Aggregator.Entities;
using ProductManagement.DTO.Response;

namespace ProductManagement.Handler.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductResponseDto>();
    }
}