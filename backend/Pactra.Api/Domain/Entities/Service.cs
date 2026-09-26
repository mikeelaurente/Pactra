namespace Pactra.Api.Domain.Entities;
public class Service
{
    public long Id { get; set; }
    public long ProviderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public User Provider { get; set; } = null!;
}