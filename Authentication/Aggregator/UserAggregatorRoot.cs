using Authentication.DTO.Command;

namespace Authentication.Aggregator;

public class UserAggregatorRoot
{
    public Guid Id { get; private set; }

    public string Username { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;


    public string PasswordHash { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTime? RefreshTokenExpiry { get; private set; }


    private static void Validate(string username, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username is required.");

        if (username.Length > 50)
            throw new ArgumentException("Username cannot exceed 50 characters.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        if (!email.Contains('@'))
            throw new ArgumentException("Email is not valid.");

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required.");

        if (password.Length < 6)
            throw new ArgumentException("Password must be at least 6 characters.");
    }

    public void Register(RegisterCommandDto command, string passwordHash)
    {
        Validate(command.Username, command.Email, command.Password);

        Id           = Guid.NewGuid();
        Username     = command.Username;
        Email        = command.Email.ToLowerInvariant();
        PasswordHash = passwordHash;
        CreatedAt    = DateTime.UtcNow;
    }

    public void SetRefreshToken(string refreshToken, DateTime expiry)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new ArgumentException("Refresh token cannot be empty.");

        RefreshToken       = refreshToken;
        RefreshTokenExpiry = expiry;
    }

    public void ClearRefreshToken()
    {
        RefreshToken       = null;
        RefreshTokenExpiry = null;
    }
}
