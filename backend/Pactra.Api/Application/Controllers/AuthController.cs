using Microsoft.AspNetCore.Mvc;
using Pactra.Api.Application.DTOs.Authentication;
using Pactra.Api.Application.Interfaces;

namespace Pactra.Api.Application.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);

        return Ok(new
        {
            accessToken = token
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (!result)
        {
            return Conflict("User already exists");
        }

        return Ok(new
        {
            message = "User registered successfully."
        });
    }
}