using ProductManagement.DTO;
using ProductManagement.DTO.Response;

namespace ProductManagement.DTO.Command;

public class CreateProductCommandDto : ICommand<ProductResponseDto>
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Quantity { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal Price { get; set; }
}
