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
}