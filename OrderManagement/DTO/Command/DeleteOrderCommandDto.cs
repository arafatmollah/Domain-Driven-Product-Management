namespace OrderManagement.DTO.Command;

public class DeleteOrderCommandDto : ICommand
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();

    public int Id { get; set; }
}
