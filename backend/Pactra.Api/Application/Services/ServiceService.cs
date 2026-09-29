using Microsoft.EntityFrameworkCore;
using Pactra.Api.Application.DTOs.Services;
using Pactra.Api.Application.Interfaces;
using Pactra.Api.Domain.Entities.Catalog;
using Pactra.Api.Domain.Enums;
using Pactra.Api.Infrastructure.Persistence;

namespace Pactra.Api.Application.Services;

public class ServiceService(PactraDbContext db) : IServiceService
{
    private readonly PactraDbContext _db = db;

    public async Task<ServiceDto> CreateAsync(long providerId, CreateServiceRequest request)
    {
        var service = new Service
        {
            ProviderId = providerId,
            Name = request.Name,
            Description = request.Description,
            BasePrice = request.BasePrice,
            Status = ServiceStatus.Active,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _db.Services.Add(service);
        await _db.SaveChangesAsync();

        return MapToDto(service);
    }

    public async Task<ServiceDto> GetAsync(long serviceId)
    {
        var service = await _db.Services
            .FirstOrDefaultAsync(s => s.Id == serviceId)
            ?? throw new KeyNotFoundException("Service not found.");

        return MapToDto(service);
    }

    public async Task<List<ServiceDto>> ListAsync(long? providerId = null)
    {
        var query = _db.Services.AsQueryable();

        if (providerId.HasValue)
        {
            query = query.Where(s => s.ProviderId == providerId.Value);
        }

        var services = await query.OrderByDescending(s => s.CreatedAt).ToListAsync();
        return [.. services.Select(MapToDto)];
    }

    public async Task<ServiceDto> UpdateAsync(long serviceId, long providerId, UpdateServiceRequest request)
    {
        var service = await _db.Services
            .FirstOrDefaultAsync(s => s.Id == serviceId)
            ?? throw new KeyNotFoundException("Service not found.");

        if (service.ProviderId != providerId)
        {
            throw new UnauthorizedAccessException("You can only update your own services.");
        }

        if (!string.IsNullOrEmpty(request.Name))
            service.Name = request.Name;

        if (!string.IsNullOrEmpty(request.Description))
            service.Description = request.Description;

        if (request.BasePrice.HasValue && request.BasePrice > 0)
            service.BasePrice = request.BasePrice.Value;

        service.UpdatedAt = DateTimeOffset.UtcNow;

        await _db.SaveChangesAsync();

        return MapToDto(service);
    }

    public async Task DeleteAsync(long serviceId, long providerId)
    {
        var service = await _db.Services
            .FirstOrDefaultAsync(s => s.Id == serviceId)
            ?? throw new KeyNotFoundException("Service not found.");

        if (service.ProviderId != providerId)
        {
            throw new UnauthorizedAccessException("You can only delete your own services.");
        }

        _db.Services.Remove(service);
        await _db.SaveChangesAsync();
    }

    private static ServiceDto MapToDto(Service service)
    {
        return new ServiceDto
        {
            Id = service.Id,
            ProviderId = service.ProviderId,
            Name = service.Name,
            Description = service.Description,
            Status = service.Status,
            BasePrice = service.BasePrice,
            CreatedAt = service.CreatedAt,
            UpdatedAt = service.UpdatedAt
        };
    }
}
