using Microsoft.EntityFrameworkCore;
using Pactra.Api.Application.DTOs;
using Pactra.Api.Application.Interfaces;
using Pactra.Api.Domain.Entities.Engagement;
using Pactra.Api.Domain.Enums;
using Pactra.Api.Infrastructure.Persistence;

namespace Pactra.Api.Application.Services;

public class EngagementService(PactraDbContext db) : IEngagementService
{
    private readonly PactraDbContext _db = db;

    public async Task<Engagement> CreateAsync(
        long serviceId,
        long clientId,
        CreateEngagementRequest request)
    {
        var service = await _db.Services
            .FirstOrDefaultAsync(s => s.Id == serviceId) ?? throw new KeyNotFoundException("Service not found.");
        
        if (clientId == service.ProviderId)
        {
            throw new InvalidOperationException(
                "The client cannot be the provider of the service.");
        }

        if (service.Status != ServiceStatus.Active)
        {
            throw new InvalidOperationException(
                "This service is not currently available.");
        }

        var engagement = new Engagement
        {
            ServiceId = service.Id,
            ClientId = clientId,
            ProviderId = service.ProviderId,

            Description = request.Description,
            Goals = request.Goals,
            RequestedFeatures = request.RequestedFeatures,
            Constraints = request.Constraints,
            Budget = request.Budget,
            DesiredStartDate = request.DesiredStartDate,
            AdditionalInfo = request.AdditionalInfo,

            Status = EngagementStatus.Requested,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _db.Engagements.Add(engagement);

        await _db.SaveChangesAsync();

        return engagement;
    }

    public async Task<Engagement> AcceptAsync(
        long serviceId,
        long engagementId,
        long providerId)
    {
        var engagement = await _db.Engagements
            .FirstOrDefaultAsync(e => e.Id == engagementId && e.ServiceId == serviceId) 
            ?? throw new KeyNotFoundException("Engagement not found.");

        if (engagement.ProviderId != providerId)
        {
            throw new InvalidOperationException(
                "The provider is not authorized to accept this engagement.");
        }

        if (engagement.Status != EngagementStatus.Requested)
        {
            throw new InvalidOperationException(
                "Only requested engagements can be accepted.");
        }

        engagement.Status = EngagementStatus.Negotiating;
        engagement.UpdatedAt = DateTimeOffset.UtcNow;

        await _db.SaveChangesAsync();

        return engagement;
    }
    
    public async Task<Engagement> RejectAsync(
        long serviceId,
        long engagementId,
        long providerId)
    {
        var engagement = await _db.Engagements
            .FirstOrDefaultAsync(e => e.Id == engagementId && e.ServiceId == serviceId) 
            ?? throw new KeyNotFoundException("Engagement not found.");

        if (engagement.ProviderId != providerId)
        {
            throw new InvalidOperationException(
                "The provider is not authorized to reject this engagement.");
        }

        if (engagement.Status != EngagementStatus.Requested)
        {
            throw new InvalidOperationException(
                "Only requested engagements can be rejected.");
        }

        engagement.Status = EngagementStatus.Rejected;
        engagement.UpdatedAt = DateTimeOffset.UtcNow;

        await _db.SaveChangesAsync();

        return engagement;
    }
}