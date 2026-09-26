namespace Pactra.Api.Domain.Entities;

public class Session
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string RefreshTokenHash { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }

    public User User { get; set; } = null!;
}