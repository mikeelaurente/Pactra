using Microsoft.EntityFrameworkCore;
using Pactra.Api.Application.Services;
using Pactra.Api.Domain.Entities.Engagement;
using Pactra.Api.Domain.Enums;
using Pactra.Api.Infrastructure.Persistence;

namespace Pactra.Tests.Application.Services;

public class EngagementServiceTests
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
    public async Task AcceptAsync_WhenEngagementDoesNotExist_ThrowsException()
    {
        await using var db = CreateDbContext();
        var service = new EngagementService(db);

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.AcceptAsync(1, 999, 1));

        Assert.Equal("Engagement not found.", exception.Message);
    }

    [Fact]
    public async Task AcceptAsync_WithValidProvider_ChangesStatusToNegotiating()
    {
        await using var db = CreateDbContext();

        var engagement = new Engagement
        {
            Id = 10,
            ServiceId = 1,
            ProviderId = 5,
            Status = EngagementStatus.Requested
        };

        db.Engagements.Add(engagement);
        await db.SaveChangesAsync();

        var service = new EngagementService(db);

        var result = await service.AcceptAsync(1, 10, 5);

        await using var verifiedDb = CreateDbContext();

        var savedEngagement = await verifiedDb.Engagements.FindAsync(10L);

        Assert.NotNull(savedEngagement);

        Assert.Equal(EngagementStatus.Negotiating, result.Status);
        Assert.Equal(EngagementStatus.Negotiating, savedEngagement.Status);
    }

    [Fact]
    public async Task AcceptAsync_WhenEngagementExistsAndProviderIsNotAuthorized_ThrowsException()
    {
        await using var db = CreateDbContext();

        var engagement = new Engagement
        {
            Id = 20,
            ServiceId = 1,
            ProviderId = 1,
            Status = EngagementStatus.Requested
        };

        db.Engagements.Add(engagement);
        await db.SaveChangesAsync();

        var service = new EngagementService(db);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.AcceptAsync(
                serviceId: 1,
                engagementId: 20,
                providerId: 999
            )
        );

        Assert.Equal(EngagementStatus.Requested, engagement.Status);
        Assert.Equal("The provider is not authorized to accept this engagement.", exception.Message);
    }

    [Fact]
    public async Task AcceptAsync_WhenEngagementStatusIsNotRequested_ThrowsException()
    {
        await using var db = CreateDbContext();

        var engagement = new Engagement
        {
            Id = 30,
            ServiceId = 1,
            ProviderId = 5,
            Status = EngagementStatus.Negotiating
        };

        db.Engagements.Add(engagement);
        await db.SaveChangesAsync();

        var service = new EngagementService(db);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.AcceptAsync(1, 30, 5)
        );

        Assert.Equal("Only requested engagements can be accepted.", exception.Message);
        Assert.Equal(EngagementStatus.Negotiating, engagement.Status);
    }

    [Fact]
    public async Task AcceptAsync_WhenServiceIdDoesNotMatch_ThrowsException()
    {
        await using var db = CreateDbContext();

        var engagement = new Engagement
        {
            Id = 40,
            ServiceId = 1,
            ProviderId = 5,
            Status = EngagementStatus.Requested
        };

        db.Engagements.Add(engagement);
        await db.SaveChangesAsync();

        var service = new EngagementService(db);

        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.AcceptAsync(
                serviceId: 999,
                engagementId: 40,
                providerId: 5
            )
        );

        Assert.Equal(EngagementStatus.Requested, engagement.Status);
        Assert.Equal("Engagement not found.", exception.Message);
    }
}