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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>(entity =>
    {
        entity.HasKey(user => user.Id);

        entity.Property(user => user.Name)
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(user => user.Email)
            .HasMaxLength(255)
            .IsRequired();

        entity.HasIndex(user => user.Email)
            .IsUnique();

        entity.Property(user => user.PasswordHash)
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(user => user.CreatedAt)
            .IsRequired();

        entity.Property(user => user.UpdatedAt)
            .IsRequired();
    });

    modelBuilder.Entity<Role>(entity =>
    {
        entity.HasKey(role => role.Id);

        entity.Property(role => role.Name)
            .HasMaxLength(50)
            .IsRequired();

        entity.HasIndex(role => role.Name)
            .IsUnique();
    });

    modelBuilder.Entity<UserRole>(entity =>
    {
        entity.HasKey(userRole => new 
        { 
            userRole.UserId, 
            userRole.RoleId 
        });

        entity.HasOne(userRole => userRole.User)
            .WithMany()
            .HasForeignKey(userRole => userRole.UserId);

        entity.HasOne(userRole => userRole.Role)
            .WithMany()
            .HasForeignKey(userRole => userRole.RoleId);
    });
}
}