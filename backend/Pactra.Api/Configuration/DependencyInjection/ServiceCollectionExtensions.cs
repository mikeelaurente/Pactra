using Pactra.Api.Application.Interfaces;
using Pactra.Api.Application.Services;
using Pactra.Api.Infrastructure.Authentication;

namespace Pactra.Api.Configuration.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IEngagementService, EngagementService>();

        return services;
    }
}