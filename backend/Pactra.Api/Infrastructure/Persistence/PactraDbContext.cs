using Microsoft.EntityFrameworkCore;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence;

public class PactraDbContext : DbContext
{
    public PactraDbContext(DbContextOptions<PactraDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Service> Services => Set<Service>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PactraDbContext).Assembly
        );
    }
}