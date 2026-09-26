namespace Pactra.Api.Domain.Entities;

public class EngagementStatusHistory
{
    public long Id { get; set; }
    public long EngagementId { get; set; }
    public string FromStatus { get; set; } = string.Empty;
    public string ToStatus { get; set; } = string.Empty;
    public long? ChangedBy { get; set; }
    public DateTimeOffset ChangedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;
    public User? Actor { get; set; } = null!;
}