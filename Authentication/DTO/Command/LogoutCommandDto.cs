namespace Authentication.DTO.Command;

public class LogoutCommandDto : ICommand
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();

    public Guid UserId { get; set; }
}
