using System.Security.Claims;

namespace Pactra.Api.Application.Authorization;

public static class ClaimsPrincipalExtensions
{
    public static long GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (claim is null)
        {
            throw new InvalidOperationException("User ID claim is missing.");
        }

        return long.Parse(claim.Value);
    }
}