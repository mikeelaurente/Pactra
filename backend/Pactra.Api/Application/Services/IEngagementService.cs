using Pactra.Api.Application.DTOs;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Application.Services;

public interface IEngagementService
{
    Task<Engagement> CreateAsync(
        long serviceId,
        long clientId,
        CreateEngagementRequest request);
}
