using Pactra.Api.Domain.Enums;

namespace Pactra.Api.Domain.Entities;
public class Service
{
    public long Id { get; set; }
    public long ProviderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ServiceStatus Status { get; set; }
    public decimal BasePrice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public User Provider { get; set; } = null!;
}