using ProductManagement.DTO;
using ProductManagement.DTO.Response;

namespace ProductManagement.DTO.Query;

public class GetProductsQuery : IQuery<IEnumerable<ProductResponseDto>>
{
}
