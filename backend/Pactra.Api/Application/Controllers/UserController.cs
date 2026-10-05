using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pactra.Api.Application.Authorization;
using Pactra.Api.Application.Interfaces;

namespace Pactra.Api.Application.Controllers;

[Authorize]
[ApiController]
[Route("users/me")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("become-provider")]
    public async Task<IActionResult> BecomeProvider()
    {
        var userID = User.GetUserId();

        var result = await _userService.BecomeProviderAsync(userID);
        if (!result)
        {
            return Conflict("User is already a provider");
        }

        return Ok(new
        {
            message = "User became a provider successfully."
        });
    }
}