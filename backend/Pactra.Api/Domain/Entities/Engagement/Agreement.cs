using Pactra.Api.Domain.Enums;

namespace Pactra.Api.Domain.Entities;
public class Agreement
{
    public long Id { get; set; }
    public long EngagementId { get; set; }
    public int Version { get; set; }
    public AgreementStatus Status { get; set; } = AgreementStatus.Draft;
    public DateTimeOffset CreatedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;
}