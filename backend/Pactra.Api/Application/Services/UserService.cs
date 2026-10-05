using Microsoft.EntityFrameworkCore;
using Pactra.Api.Application.Interfaces;
using Pactra.Api.Domain.Entities.Authentication;
using Pactra.Api.Infrastructure.Persistence;

namespace Pactra.Api.Application.Services;

public class UserService(PactraDbContext db) : IUserService
{
    private readonly PactraDbContext _db = db;

    public async Task<bool> BecomeProviderAsync(long userID)
    {
        var user = await _db.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.Id == userID)
            ?? throw new InvalidOperationException("User not found");

        var providerRole = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "PROVIDER")
            ?? throw new InvalidOperationException("Provider role not found");

        if (user.UserRoles.Any(ur => ur.RoleId == providerRole.Id))
        {
            return false; 
        }

        var newUserRole = new UserRole
        {
            User = user,
            Role = providerRole
        };

        _db.UserRoles.Add(newUserRole);
        await _db.SaveChangesAsync();

        return true;
    }
}