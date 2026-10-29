using Pactra.Api.Domain.Entities.Engagement;
using Pactra.Api.Domain.Enums;

namespace Pactra.Tests.Domain.Entities;

public class EngagementTests
{
    [Fact]
    public void Accept_WhenStatusIsRequested_ChangeToNegotiating()
    {
        var engagement = new Engagement
        {
            Status = EngagementStatus.Requested
        };

        engagement.Accept();

        Assert.Equal(EngagementStatus.Negotiating, engagement.Status);
    }

    [Fact]
    public void Accept_WhenStatusIsNotRequested_ThrowException()
    {
        var engagement = new Engagement
        {
            Status = EngagementStatus.Negotiating
        };

        var exception = Assert.Throws<InvalidOperationException>(() => engagement.Accept());

        Assert.Equal("Only requested engagements can be accepted.", exception.Message);
    }
}