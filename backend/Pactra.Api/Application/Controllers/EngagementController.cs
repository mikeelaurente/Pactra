using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pactra.Api.Application.Authorization;
using Pactra.Api.Application.DTOs;
using Pactra.Api.Application.Interfaces;
using Pactra.Api.Domain.Entities.Engagement;

namespace Pactra.Api.Application.Controllers;

[Authorize]
[ApiController]
[Route("services/{serviceId:long}/engagements")]
public class EngagementsController(IEngagementService engagementService) : ControllerBase
{
    private readonly IEngagementService _engagementService = engagementService;

    [Authorize(Roles = "CLIENT")]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromRoute] long serviceId,
        [FromBody] CreateEngagementRequest request)
    {
        var clientId = User.GetUserId();
        var engagement = await _engagementService.CreateAsync(serviceId, clientId, request);
        return StatusCode(StatusCodes.Status201Created, engagement);
    }

    [Authorize(Roles = "PROVIDER")]
    [HttpPost("{engagementId:long}/accept")]
    public async Task<IActionResult> Accept(
        long serviceId,
        long engagementId)
    {
        var providerId = User.GetUserId();

        var engagement = await _engagementService.AcceptAsync(
            serviceId,
            engagementId,
            providerId);

        return Ok(engagement);
    }

    [Authorize(Roles = "PROVIDER")]
    [HttpPost("{engagementId:long}/reject")]
    public async Task<IActionResult> Reject(
        long serviceId,
        long engagementId)
    {
        var providerId = User.GetUserId();

        var engagement = await _engagementService.RejectAsync(
            serviceId,
            engagementId,
            providerId);

        return Ok(engagement);
    }
}
