namespace Authentication.DTO.Command;

public class RegisterCommandDto : ICommand
{

    public Guid CorrelationId { get; init; } = Guid.NewGuid();

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Guid UserId { get; set; }
}
