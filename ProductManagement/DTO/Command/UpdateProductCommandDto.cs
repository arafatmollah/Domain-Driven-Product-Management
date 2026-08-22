using ProductManagement.DTO;
using ProductManagement.DTO.Response;

namespace ProductManagement.DTO.Command;

public class UpdateProductCommandDto : ICommand<ProductResponseDto>
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public DateTime ExpirationDate { get; set; }

    public decimal Price { get; set; }
}
