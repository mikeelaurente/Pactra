using Pactra.Api.Application.DTOs.Proposal;

namespace Pactra.Api.Application.Interfaces;

public interface IProposalService
{
    Task<ProposalDto> CreateProposalAsync(long engagementId,long userId, CreateProposalDto createProposalDto);
    Task<ProposalDto> AcceptProposalAsync(long engagementId, long proposalId, long userId);
}