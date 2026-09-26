namespace Pactra.Api.Domain.Entities;

public class CancellationRequest
{
    public long Id { get; set; }
    public long EngagementId { get; set; }
    public long RequestedBy { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset RequestedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;
    public User Requester { get; set; } = null!;
}