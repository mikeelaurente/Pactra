using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pactra.Api.Application.DTOs;
using Pactra.Api.Application.Services;
using Pactra.Api.Authorization;

namespace Pactra.Api.Application.Controllers;

[Authorize]
[ApiController]
[Route("services/{serviceId:long}/engagements")]
public class EngagementsController(IEngagementService engagementService) : ControllerBase
{
    private readonly IEngagementService _engagementService = engagementService;

    [HttpPost]
    public async Task<IActionResult> Create(
        long serviceId,
        CreateEngagementRequest request)
    {
        var clientId = User.GetUserId();

        var engagement = await _engagementService.CreateAsync(
            serviceId,
            clientId,
            request);

        return Ok(engagement);
    }

}
