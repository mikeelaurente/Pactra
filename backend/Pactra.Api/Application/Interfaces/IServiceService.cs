using Pactra.Api.Application.DTOs.Services;

namespace Pactra.Api.Application.Interfaces;

public interface IServiceService
{
    Task<ServiceDto> CreateAsync(long providerId, CreateServiceRequest request);
    Task<ServiceDto> GetAsync(long serviceId);
    Task<List<ServiceDto>> ListAsync(long? providerId = null);
    Task<ServiceDto> UpdateAsync(long serviceId, long providerId, UpdateServiceRequest request);
    Task DeleteAsync(long serviceId, long providerId);
}
