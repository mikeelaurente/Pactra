using Microsoft.EntityFrameworkCore;
using Pactra.Api.Application.Services;
using Pactra.Api.Domain.Entities.Engagement;
using Pactra.Api.Domain.Enums;
using Pactra.Api.Infrastructure.Persistence;

namespace Pactra.Tests.Application.Services;

public class ProposalServiceTests
{

    private readonly string _databaseName = Guid.NewGuid().ToString();

    private PactraDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<PactraDbContext>()
            .UseInMemoryDatabase(_databaseName)
            .Options;

        return new PactraDbContext(options);
    }

    [Fact]
    public async Task AcceptProposalAsync_WithValidProposal_UpdatesWorkflow()
    {
        await using var db = CreateDbContext();

        var engagement = new Engagement
        {
            Id = 10,
            ServiceId = 1,
            ClientId = 2,
            ProviderId = 5,
            Status = EngagementStatus.Negotiating
        };

        var proposal = new Proposal
        {
            Id = 20,
            EngagementId = 10,
            ProposedBy = 5,
            Status = ProposalStatus.Pending,
            Amount = 5000,
            CreatedAt = DateTimeOffset.UtcNow
        };

        db.Engagements.Add(engagement);
        db.Proposals.Add(proposal);
        await db.SaveChangesAsync();

        var proposalService = new ProposalService(db);

        await using var verifiedDb = CreateDbContext();

        var result = await proposalService.AcceptProposalAsync(10, 20, 2); 
        var verifiedProposal = await verifiedDb.Proposals.FindAsync(20L);

        Assert.NotNull(verifiedProposal);

        Assert.Equal(ProposalStatus.Accepted, result.Status);
        Assert.Equal(ProposalStatus.Accepted, verifiedProposal.Status);

        var verifiedEngagement = await verifiedDb.Engagements.FindAsync(10L);
        Assert.NotNull(verifiedEngagement);
        Assert.Equal(EngagementStatus.AgreementPending, verifiedEngagement.Status);

        var verifiedAgreement = await verifiedDb.Agreements.SingleOrDefaultAsync(a => a.EngagementId == 10);
        Assert.NotNull(verifiedAgreement);
        Assert.Equal(AgreementStatus.PendingSignatures, verifiedAgreement.Status);

        Assert.Equal(1, verifiedAgreement.Version);
    }

    [Fact]
    public async Task AcceptProposalAsync_WhenAccepted_SupersedesOnlyPendingProposals()
    {
        await using var db = CreateDbContext();

        var engagement = new Engagement
        {
            Id = 10,
            ServiceId = 1,
            ClientId = 2,
            ProviderId = 5,
            Status = EngagementStatus.Negotiating
        };

        var proposal = new Proposal
        {
            Id = 20,
            EngagementId = 10,
            ProposedBy = 5,
            Status = ProposalStatus.Pending,
            Amount = 5000,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var proposal2 = new Proposal
        {
            Id = 21,
            EngagementId = 10,
            ProposedBy = 5,
            Status = ProposalStatus.Pending,
            Amount = 6000,
            CreatedAt = DateTimeOffset.UtcNow
        };

        var proposal3 = new Proposal
        {
            Id = 22,
            EngagementId = 10,
            ProposedBy = 5,
            Status = ProposalStatus.Rejected,
            Amount = 7000,
            CreatedAt = DateTimeOffset.UtcNow
        };

        db.Engagements.Add(engagement);
        db.Proposals.AddRange(
            proposal,
            proposal2,
            proposal3
        );
        await db.SaveChangesAsync();

        var proposalService = new ProposalService(db);

        await proposalService.AcceptProposalAsync(10, 20, 2);

        await using var verifiedDb = CreateDbContext();

        var verifiedProposal = await verifiedDb.Proposals.FindAsync(20L);
        var verifiedProposal2 = await verifiedDb.Proposals.FindAsync(21L);
        var verifiedProposal3 = await verifiedDb.Proposals.FindAsync(22L);

        Assert.NotNull(verifiedProposal);
        Assert.NotNull(verifiedProposal2);
        Assert.NotNull(verifiedProposal3);

        Assert.Equal(ProposalStatus.Accepted, verifiedProposal.Status);
        Assert.Equal(ProposalStatus.Superseded, verifiedProposal2.Status);
        Assert.Equal(ProposalStatus.Rejected, verifiedProposal3.Status);

        var hasPendingProposals = await verifiedDb.Proposals
        .AnyAsync(p =>
            p.EngagementId == 10 &&
            p.Status == ProposalStatus.Pending);

        Assert.False(hasPendingProposals);
    }

    [Fact]
    public async Task AcceptProposalAsync_WhenProposalIsExpired_ThrowsException()
    {
        await using var db = CreateDbContext();

        var engagement = new Engagement
        {
            Id = 10,
            ServiceId = 1,
            ClientId = 2,
            ProviderId = 5,
            Status = EngagementStatus.Negotiating
        };

        var proposal = new Proposal
        {
            Id = 20,
            EngagementId = 10,
            ProposedBy = 5,
            Status = ProposalStatus.Pending,
            Amount = 5000,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(-1),
            CreatedAt = DateTimeOffset.UtcNow.AddDays(-7)
        };

        db.Engagements.Add(engagement);
        db.Proposals.Add(proposal);

        await db.SaveChangesAsync();

        var service = new ProposalService(db);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.AcceptProposalAsync(10, 20, 2));

        Assert.Equal(EngagementStatus.Negotiating, engagement.Status);
        Assert.Equal(ProposalStatus.Pending, proposal.Status);
        Assert.Equal("Cannot accept a proposal that has expired.", exception.Message);

        await using var verifiedDb = CreateDbContext();

        var savedEngagement = await verifiedDb.Engagements.FindAsync(10L);
        var savedProposal = await verifiedDb.Proposals.FindAsync(20L);

        Assert.NotNull(savedEngagement);
        Assert.NotNull(savedProposal);

        Assert.Equal(EngagementStatus.Negotiating, savedEngagement.Status);
        Assert.Equal(ProposalStatus.Pending, savedProposal.Status);

        var agreementExists = await verifiedDb.Agreements.AnyAsync(a => a.EngagementId == 10);
        Assert.False(agreementExists);  
    }

    [Fact]
    public async Task AcceptProposalAsync_WhenUserAcceptsOwnProposal_ThrowsException()
    {
        await using var db = CreateDbContext();
        var engagement = new Engagement
        {
            Id = 10,
            ServiceId = 1,
            ClientId = 2,
            ProviderId = 5,
            Status = EngagementStatus.Negotiating
        };

        var proposal = new Proposal
        {
            Id = 20,
            EngagementId = 10,
            ProposedBy = 5,
            Status = ProposalStatus.Pending,
            Amount = 5000,
            CreatedAt = DateTimeOffset.UtcNow
        };

        db.Engagements.Add(engagement);
        db.Proposals.Add(proposal);

        await db.SaveChangesAsync();
        
        var service = new ProposalService(db);
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.AcceptProposalAsync(10, 20, 5));

        Assert.Equal(EngagementStatus.Negotiating, engagement.Status);
        Assert.Equal(ProposalStatus.Pending, proposal.Status);
        Assert.Equal("User with ID 5 cannot accept their own proposal.", exception.Message);

        await using var verifiedDb = CreateDbContext();

        var savedEngagement = await verifiedDb.Engagements.FindAsync(10L);
        var savedProposal = await verifiedDb.Proposals.FindAsync(20L);

        Assert.NotNull(savedEngagement);
        Assert.NotNull(savedProposal);

        Assert.Equal(EngagementStatus.Negotiating, savedEngagement.Status);
        Assert.Equal(ProposalStatus.Pending, savedProposal.Status);

        var agreementExists = await verifiedDb.Agreements.AnyAsync(a => a.EngagementId == 10);
        Assert.False(agreementExists); 
    }
    [Fact]
public void GetEffectiveStatus_WhenPendingProposalIsExpired_ReturnsExpired()
{
    var now = DateTimeOffset.UtcNow;

    var proposal = new Proposal
    {
        Status = ProposalStatus.Pending,
        ExpiresAt = now.AddDays(-1)
    };

    var result = proposal.GetEffectiveStatus(now);

    Assert.Equal(ProposalStatus.Expired, result);
    Assert.Equal(ProposalStatus.Pending, proposal.Status);
}
}