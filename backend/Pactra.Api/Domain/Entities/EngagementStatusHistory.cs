using Pactra.Api.Domain.Enum;

namespace Pactra.Api.Domain.Entities;

public class EngagementStatusHistory
{
    public long Id { get; set; }
    public long EngagementId { get; set; }
    public EngagementStatus FromStatus { get; set; }
    public EngagementStatus ToStatus { get; set; }
    public long? ChangedBy { get; set; }
    public DateTimeOffset ChangedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;
    public User? Actor { get; set; }
}