using AutoMapper;
using OrderManagement.Aggregator;
using OrderManagement.DTO.Response;

namespace OrderManagement.Handler.Mapping;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderAggregatorRoot, OrderResponseDto>();
    }
}
