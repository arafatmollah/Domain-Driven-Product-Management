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
    [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommandDto request,
        CancellationToken cancellationToken)
    {
        await serviceBus.SendCommandAsync(request, cancellationToken);

        return Ok(request.Result);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshTokenCommandDto request,
        CancellationToken cancellationToken)
    {
        await serviceBus.SendCommandAsync(request, cancellationToken);

        return Ok(request.Result);
    }

    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutCommandDto request,
        CancellationToken cancellationToken)
    {
        await serviceBus.SendCommandAsync(request, cancellationToken);

        return NoContent();
    }
}
