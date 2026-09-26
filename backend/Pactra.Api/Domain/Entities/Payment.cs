using Pactra.Api.Domain.Enum;

namespace Pactra.Api.Domain.Entities;

public class Payment
{
    public long Id { get; set; }

    public long EngagementId { get; set; }

    public PaymentType Type { get; set; }

    public decimal Amount { get; set; }

    public string? ReferenceNumber { get; set; }

    public DateTimeOffset? SubmittedAt { get; set; }

    public DateTimeOffset? VerifiedAt { get; set; }

    public long? VerifiedBy { get; set; }

    public PaymentStatus Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;

    public User? Verifier { get; set; }
}