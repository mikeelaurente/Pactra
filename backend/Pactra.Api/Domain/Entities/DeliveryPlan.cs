namespace Pactra.Api.Domain.Entities;

public class DeliveryPlan
{
    public long Id { get; set; }

    public long EngagementId { get; set; }

    public string Scope { get; set; } = string.Empty;
    public string? Deliverables { get; set; }
    public string? Milestones { get; set; }
    public string? Timeline { get; set; }
    public string? Assumptions { get; set; }
    public string? Exclusions { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;
}