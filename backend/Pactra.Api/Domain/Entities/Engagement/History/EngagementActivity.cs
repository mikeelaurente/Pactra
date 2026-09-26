using Pactra.Api.Domain.Enums;

namespace Pactra.Api.Domain.Entities;
public class EngagementActivity
{
    public long Id { get; set; }

    public long EngagementId { get; set; }

    public long? ActorId { get; set; }

    public EngagementActivityType Type { get; set; }

    public string? Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Engagement Engagement { get; set; } = null!;
    public User? Actor { get; set; }
}