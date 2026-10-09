using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pactra.Api.Application.Authorization;
using Pactra.Api.Application.Interfaces;

namespace Pactra.Api.Application.Controllers;

[ApiController]
[Route("agreements")]
public class AgreementController(IAgreementService agreementService) : ControllerBase
{
    private readonly IAgreementService _agreementService = agreementService;

    [Authorize(Roles = "CLIENT,PROVIDER")]
    [HttpPost]
    [Route("{agreementId:long}/sign")]
    public async Task<IActionResult> SignAgreement([FromRoute] long agreementId)
    {
        var userId = User.GetUserId();
        await _agreementService.SignAgreementAsync(agreementId, userId);
        return NoContent();
    }
}