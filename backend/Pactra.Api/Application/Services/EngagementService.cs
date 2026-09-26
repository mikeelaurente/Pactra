using Microsoft.EntityFrameworkCore;
using Pactra.Api.Application.DTOs;
using Pactra.Api.Domain.Entities;
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
}