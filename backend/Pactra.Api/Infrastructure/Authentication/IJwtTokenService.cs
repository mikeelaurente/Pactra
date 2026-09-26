using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Authentication;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
}