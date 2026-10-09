using Microsoft.EntityFrameworkCore;
using Pactra.Api.Application.DTOs.Proposal;
using Pactra.Api.Application.Interfaces;
using Pactra.Api.Domain.Entities.Engagement;
using Pactra.Api.Domain.Enums;
using Pactra.Api.Infrastructure.Persistence;

namespace Pactra.Api.Application.Services;

public class ProposalService(PactraDbContext _db) : IProposalService
{
    private readonly PactraDbContext _db = _db;
    public async Task<ProposalDto> CreateProposalAsync(long engagementId, long userId, CreateProposalDto createProposalDto)
    {
        var engagement = await _db.Engagements.FindAsync(engagementId) 
        ?? throw new KeyNotFoundException($"Engagement with ID {engagementId} not found.");

        if (engagement.Status != EngagementStatus.Negotiating)
        {
            throw new InvalidOperationException($"Cannot create a proposal for an engagement that is not in the negotiating state.");
        }

        if (createProposalDto.ExpiresAt.HasValue &&
            createProposalDto.ExpiresAt.Value <= DateTimeOffset.UtcNow)
        {
            throw new InvalidOperationException(
                "Proposal expiration must be in the future.");
        }

        if (engagement.ClientId != userId && engagement.ProviderId != userId)
        {
            throw new InvalidOperationException($"User with ID {userId} is not part of this engagement.");
        }

        var proposal = new Proposal
        {
            EngagementId = engagement.Id,
            ProposedBy = userId,
            Amount = createProposalDto.Amount,
            Description = createProposalDto.Description,
            Terms = createProposalDto.Terms,
            Status = ProposalStatus.Pending,
            ExpiresAt = createProposalDto.ExpiresAt,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _db.Proposals.Add(proposal);
        await _db.SaveChangesAsync();

        return new ProposalDto
        {
            Id = proposal.Id,
            EngagementId = proposal.EngagementId,
            ProposedBy = proposal.ProposedBy,
            Amount = proposal.Amount,
            Description = proposal.Description,
            Terms = proposal.Terms,
            Status = proposal.Status,
            ExpiresAt = proposal.ExpiresAt,
            CreatedAt = proposal.CreatedAt
        };
    }

    public async Task<ProposalDto> AcceptProposalAsync(long engagementId, long proposalId, long userId)
    {
        var engagement = await _db.Engagements.FindAsync(engagementId)
            ?? throw new KeyNotFoundException($"Engagement with ID {engagementId} not found.");

        if (engagement.Status != EngagementStatus.Negotiating)
        {
            throw new InvalidOperationException($"Cannot accept a proposal for an engagement that is not in the negotiating state.");
        }

        if (engagement.ClientId != userId && engagement.ProviderId != userId)
        {
            throw new InvalidOperationException($"User with ID {userId} is not part of this engagement.");
        }

       var proposal = await _db.Proposals
        .FirstOrDefaultAsync(p =>
            p.Id == proposalId &&
            p.EngagementId == engagementId)
        ?? throw new KeyNotFoundException(
            "Proposal not found for this engagement.");

        if (proposal.Status != ProposalStatus.Pending)
        {
            throw new InvalidOperationException($"Cannot accept a proposal that is not in the pending state.");
        }

        if (proposal.ExpiresAt.HasValue && proposal.ExpiresAt <= DateTimeOffset.UtcNow)
        {
            throw new InvalidOperationException($"Cannot accept a proposal that has expired.");
        }

        if (userId == proposal.ProposedBy)
        {
            throw new InvalidOperationException($"User with ID {userId} cannot accept their own proposal.");
        }

        var hasAcceptedProposal = await _db.Proposals.AnyAsync
        (p => p.EngagementId == engagementId && p.Status == ProposalStatus.Accepted);
        if (hasAcceptedProposal)
        {
            throw new InvalidOperationException($"An accepted proposal already exists for engagement with ID {engagementId}.");
        }

        var now = DateTimeOffset.UtcNow;

        proposal.Status = ProposalStatus.Accepted;
        engagement.Status = EngagementStatus.AgreementPending;

        _db.Agreements.Add(new Agreement
        {
            EngagementId = engagement.Id,
            Status = AgreementStatus.PendingSignatures,
            Version = 1,
            CreatedAt = now
        });

        await _db.SaveChangesAsync();

        return new ProposalDto
        {
            Id = proposal.Id,
            EngagementId = engagement.Id,
            ProposedBy = proposal.ProposedBy,
            Amount = proposal.Amount,
            Description = proposal.Description,
            Terms = proposal.Terms,
            Status = proposal.Status,
            ExpiresAt = proposal.ExpiresAt,
            CreatedAt = now
        };
    }
}
