namespace Pactra.Api.Domain.Entities.Authentication;

public class UserRole
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}