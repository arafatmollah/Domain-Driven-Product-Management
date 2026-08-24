using ProductManagement.DTO;

namespace ProductManagement.DTO.Command;

public class DeleteProductCommandDto : ICommand
{
    /// <inheritdoc />
    public Guid CorrelationId { get; init; } = Guid.NewGuid();

    public int Id { get; set; }
}
