using Pactra.Api.Domain.Entities.Engagement;
using Pactra.Api.Domain.Enums;

namespace Pactra.Tests.Domain.Entities;

public class ProposalTests
{
    [Fact]
    public void GetEffectiveStatus_WhenPendingProposalIsExpired_ReturnsExpired()
    {
        var now = new DateTimeOffset(
            2026, 10, 29, 12, 0, 0, TimeSpan.Zero);

        var proposal = new Proposal
        {
            Status = ProposalStatus.Pending,
            ExpiresAt = now.AddDays(-1)
        };

        var result = proposal.GetEffectiveStatus(now);

        Assert.Equal(ProposalStatus.Expired, result);
        Assert.Equal(ProposalStatus.Pending, proposal.Status);
    }

    [Fact]
    public void GetEffectiveStatus_WhenPendingProposalIsNotExpired_ReturnsPending()
    {
        var now = new DateTimeOffset(
            2026, 10, 29, 12, 0, 0, TimeSpan.Zero);

        var proposal = new Proposal
        {
            Status = ProposalStatus.Pending,
            ExpiresAt = now.AddDays(1)
        };

        var result = proposal.GetEffectiveStatus(now);

        Assert.Equal(ProposalStatus.Pending, result);
        Assert.Equal(ProposalStatus.Pending, proposal.Status);
    }

    [Fact]
    public void GetEffectiveStatus_WhenPendingProposalHasNoExpiration_ReturnsPending()
    {
        var now = new DateTimeOffset(
            2026, 10, 29, 12, 0, 0, TimeSpan.Zero);

        var proposal = new Proposal
        {
            Status = ProposalStatus.Pending,
            ExpiresAt = null
        };

        var result = proposal.GetEffectiveStatus(now);

        Assert.Equal(ProposalStatus.Pending, result);
        Assert.Equal(ProposalStatus.Pending, proposal.Status);
    }

    [Fact]
    public void GetEffectiveStatus_WhenNonPendingProposalIsExpired_ReturnsStatus()
    {
        var now = new DateTimeOffset(
            2026, 10, 29, 12, 0, 0, TimeSpan.Zero);

        var proposal = new Proposal
        {
            Status = ProposalStatus.Accepted,
            ExpiresAt = now.AddDays(-1)
        };

        var result = proposal.GetEffectiveStatus(now);

        Assert.Equal(ProposalStatus.Accepted, result);
        Assert.Equal(ProposalStatus.Accepted, proposal.Status);
    }
}