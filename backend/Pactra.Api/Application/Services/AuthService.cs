using Microsoft.EntityFrameworkCore;
using Pactra.Api.Application.DTOs.Authentication;
using Pactra.Api.Application.Interfaces;
using Pactra.Api.Domain.Entities;
using Pactra.Api.Infrastructure.Authentication;
using Pactra.Api.Infrastructure.Persistence;

namespace Pactra.Api.Application.Services;

public class AuthService(PactraDbContext db, IJwtTokenService jwtService) : IAuthService
{
    private readonly PactraDbContext _db = db;
    private readonly IJwtTokenService _jwtService = jwtService;

    public async Task<string> LoginAsync(LoginRequest request)
    {
        var user = await _db.Users
        .FirstOrDefaultAsync(
            u => u.Email == request.Email
        ) ?? throw new UnauthorizedAccessException("Invalid credentials");

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        return _jwtService.GenerateAccessToken(user);
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var userExists = await _db.Users.AnyAsync(u => u.Email == request.Email);
        if (userExists) return false;

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var newUser = new User
        {
            Email = request.Email,
            PasswordHash = hashedPassword,
            Name = request.Name
        };

        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();

        return true;
    }
}