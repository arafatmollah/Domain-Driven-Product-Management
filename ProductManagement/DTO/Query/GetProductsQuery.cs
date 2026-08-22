using ProductManagement.DTO;
using ProductManagement.DTO.Response;

namespace ProductManagement.DTO.Query;

public class GetProductsQuery : IQuery<IEnumerable<ProductResponseDto>>
{
    public string? Search { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
