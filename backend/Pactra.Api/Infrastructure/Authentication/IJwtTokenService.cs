using Pactra.Api.Domain.Entities.Authentication;

namespace Pactra.Api.Infrastructure.Authentication;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
}