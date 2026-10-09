using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pactra.Api.Application.Authorization;
using Pactra.Api.Application.DTOs.Proposal;
using Pactra.Api.Application.Interfaces;

namespace Pactra.Api.Application.Controllers;

[ApiController]
[Route("engagements/{engagementId:long}/proposals")]
public class ProposalController(IProposalService proposalService) : ControllerBase
{
    private readonly IProposalService _proposalService = proposalService;

    [Authorize(Roles = "CLIENT,PROVIDER")]
    [HttpPost]
    public async Task<IActionResult> CreateProposal(
        [FromRoute]long engagementId, 
        [FromBody] CreateProposalDto createProposalDto)
    {
        var userId = User.GetUserId();
        var proposal = await _proposalService.CreateProposalAsync(engagementId, userId, createProposalDto);
        return  StatusCode(StatusCodes.Status201Created, proposal);
    }

    [Authorize(Roles = "CLIENT,PROVIDER")]
    [HttpPost("{proposalId:long}/accept")]
    public async Task<IActionResult> AcceptProposal(
        [FromRoute] long engagementId, 
        [FromRoute] long proposalId)
    {
        var userId = User.GetUserId();
        var proposal = await _proposalService.AcceptProposalAsync(engagementId, proposalId, userId);
        return Ok(proposal);
    }
}