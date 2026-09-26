using Pactra.Api.Domain.Enums;

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

    public ProposalStatus Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;
    public User Proposer { get; set; } = null!;
}