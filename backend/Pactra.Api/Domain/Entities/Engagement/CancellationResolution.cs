namespace Pactra.Api.Domain.Entities;


public class CancellationResolution
{
    public long Id { get; set; }
    public long CancellationRequestId { get; set; }
    public long ResolvedBy { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTimeOffset ResolvedAt { get; set; }

    public CancellationRequest CancellationRequest { get; set; } = null!;
    public User Resolver { get; set; } = null!;
}
