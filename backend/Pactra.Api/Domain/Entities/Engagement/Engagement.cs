
using Pactra.Api.Domain.Enums;

namespace Pactra.Api.Domain.Entities;

public class Engagement
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public long ClientId { get; set; }
    public long ProviderId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Goals { get; set; } = string.Empty;
    public string RequestedFeatures { get; set; } = string.Empty;
    public string Constraints { get; set; } = string.Empty;
    public decimal? Budget { get; set; }
    public DateTimeOffset? DesiredStartDate { get; set; }
    public string? AdditionalInfo { get; set; }
    public EngagementStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public Service Service { get; set; } = null!;
    public User Client { get; set; } = null!;
    public User Provider { get; set; } = null!;
}