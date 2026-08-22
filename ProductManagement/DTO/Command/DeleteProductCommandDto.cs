using ProductManagement.DTO;

namespace ProductManagement.DTO.Command;

public class DeleteProductCommandDto : ICommand<bool>
{
    public int Id { get; set; }
}
