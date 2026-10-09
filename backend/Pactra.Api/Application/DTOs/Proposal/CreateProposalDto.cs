namespace Pactra.Api.Application.DTOs.Proposal;

public class CreateProposalDto
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Terms { get; set; }
    public DateTimeOffset? ExpiresAt { get; set; }
}