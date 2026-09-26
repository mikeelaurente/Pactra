using Pactra.Api.Domain.Enum;

namespace Pactra.Api.Domain.Entities;

public class CancellationRequest
{
    public long Id { get; set; }
    public long EngagementId { get; set; }
    public long RequestedBy { get; set; }
    public string Reason { get; set; } = string.Empty;
    public CancellationRequestStatus Status { get; set; }
    public DateTimeOffset RequestedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;
    public User Requester { get; set; } = null!;
}