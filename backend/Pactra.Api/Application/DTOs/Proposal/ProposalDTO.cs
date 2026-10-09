using Pactra.Api.Domain.Enums;

namespace Pactra.Api.Application.DTOs.Proposal;

public class ProposalDto
{
    public long Id { get; set; }
    public long EngagementId { get; set; }
    public long ProposedBy { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Terms { get; set; }
    public ProposalStatus Status { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}