using Microsoft.AspNetCore.Mvc;
using Pactra.Api.Application.DTOs;
using Pactra.Api.Application.Services;

namespace Pactra.Api.Application.Controllers;

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
    var clientId = 1; // TODO: Replace with actual authenticated user's ID

    var engagement = await _engagementService.CreateAsync(
        serviceId,
        clientId,
        request);

    return Ok(engagement);
}

}
