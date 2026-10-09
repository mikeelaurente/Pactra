using Pactra.Api.Application.DTOs.Proposal;

namespace Pactra.Api.Application.Interfaces;

public interface IProposalService
{
    // Task<ProposalDto> GetProposalByIdAsync(int id);
    // Task<IEnumerable<ProposalDto>> GetAllProposalsAsync();
    Task<ProposalDto> CreateProposalAsync(CreateProposalDto createProposalDto);
    // Task<ProposalDto> UpdateProposalAsync(int id, UpdateProposalDto updateProposalDto);
    // Task DeleteProposalAsync(int id);
}