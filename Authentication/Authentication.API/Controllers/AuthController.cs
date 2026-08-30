using Authentication.DTO.Command;
using Authentication.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.Handlers;

namespace Authentication.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IServiceBus serviceBus) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCommandDto request,
        CancellationToken cancellationToken)
    {
        await serviceBus.SendCommandAsync(request, cancellationToken);

        var response = new UserResponseDto
        {
            Id        = request.UserId,
            Username  = request.Username,
            Email     = request.Email,
            CreatedAt = DateTime.UtcNow,
        };

        return CreatedAtAction(nameof(Register), new { id = response.Id }, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommandDto request,
        CancellationToken cancellationToken)
    {
        await serviceBus.SendCommandAsync(request, cancellationToken);

        return Ok(request.Result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshTokenCommandDto request,
        CancellationToken cancellationToken)
    {
        await serviceBus.SendCommandAsync(request, cancellationToken);

        return Ok(request.Result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutCommandDto request,
        CancellationToken cancellationToken)
    {
        await serviceBus.SendCommandAsync(request, cancellationToken);

        return NoContent();
    }
}
