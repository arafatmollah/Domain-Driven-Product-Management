using ProductManagement.DTO;
using ProductManagement.DTO.Response;

namespace ProductManagement.DTO.Query;

public class GetProductQuery : IQuery<ProductResponseDto>
{
    public int Id { get; set; }
}
