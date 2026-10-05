using Pactra.Api.Application.DTOs;
using Pactra.Api.Domain.Entities.Engagement;

namespace Pactra.Api.Application.Interfaces;

public interface IEngagementService
{
    Task<Engagement> CreateAsync(
        long serviceId,
        long clientId,
        CreateEngagementRequest request);

    Task<Engagement> AcceptAsync(
        long serviceId,
        long engagementId,
        long providerId);

    Task<Engagement> RejectAsync(
        long serviceId,
        long engagementId,
        long providerId);
}
