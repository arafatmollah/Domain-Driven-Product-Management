using ProductManagement.DTO;

namespace ProductManagement.DTO.Command;

public class DeleteProductCommandDto : ICommand
{
    public int Id { get; set; }
}
