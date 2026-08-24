using Authentication.DTO.Response;

namespace Authentication.DTO.Command;

public class LoginCommandDto : ICommand
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public AuthResponseDto? Result { get; set; }
}
