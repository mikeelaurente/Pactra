using Pactra.Api.Domain.Enum;

namespace Pactra.Api.Domain.Entities;

public class Requirement
{
    public long Id { get; set; }

    public long EngagementId { get; set; }

    public long CreatedBy { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTimeOffset DueDate { get; set; }

    public RequirementStatus Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;

    public User Creator { get; set; } = null!;
}