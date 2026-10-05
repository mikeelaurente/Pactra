using Pactra.Api.Application.Interfaces;
using Pactra.Api.Application.Services;

namespace Pactra.Api.Configuration.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IEngagementService, EngagementService>();
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}