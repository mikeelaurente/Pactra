using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pactra.Api.Application.Authorization;
using Pactra.Api.Application.DTOs.Services;
using Pactra.Api.Application.Interfaces;

namespace Pactra.Api.Application.Controllers;

[Authorize]
[ApiController]
[Route("services")]
public class ServicesController(IServiceService serviceService) : ControllerBase
{
    private readonly IServiceService _serviceService = serviceService;

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceRequest request)
    {
        var providerId = User.GetUserId();
        var service = await _serviceService.CreateAsync(providerId, request);
        return CreatedAtAction(nameof(Get), new { id = service.Id }, service);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] long? providerId = null)
    {
        var services = await _serviceService.ListAsync(providerId);
        return Ok(services);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var service = await _serviceService.GetAsync(id);
        return Ok(service);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, UpdateServiceRequest request)
    {
        var providerId = User.GetUserId();
        var service = await _serviceService.UpdateAsync(id, providerId, request);
        return Ok(service);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var providerId = User.GetUserId();
        await _serviceService.DeleteAsync(id, providerId);
        return NoContent();
    }
}
