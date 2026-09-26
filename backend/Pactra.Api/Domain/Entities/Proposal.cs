namespace Pactra.Api.Domain.Entities;

public class Proposal
{
    public long Id { get; set; }
    public long EngagementId { get; set; }
    public long ProposedBy { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Terms { get; set; }

    public DateTimeOffset? ExpiresAt { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;
    public User Proposer { get; set; } = null!;
}