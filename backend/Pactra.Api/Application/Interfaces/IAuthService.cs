using Pactra.Api.Application.DTOs.Authentication;

namespace Pactra.Api.Application.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginRequest request);
    Task<bool> RegisterAsync(RegisterRequest request);
}