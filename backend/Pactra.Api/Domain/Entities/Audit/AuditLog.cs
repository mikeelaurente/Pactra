using Pactra.Api.Domain.Enum;

namespace Pactra.Api.Domain.Entities;

public class AuditLog
{
    public long Id { get; set; }

    public long? ActorId { get; set; }

    public AuditAction Action { get; set; }

    public string EntityType { get; set; } = string.Empty;

    public long EntityId { get; set; }

    public string? Description { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public User? Actor { get; set; }
}