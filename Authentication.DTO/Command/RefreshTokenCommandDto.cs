using Authentication.DTO.Response;

namespace Authentication.DTO.Command;

public class RefreshTokenCommandDto : ICommand
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();

    public string RefreshToken { get; set; } = string.Empty;

    public AuthResponseDto? Result { get; set; }
}
