using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> entity)
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
    }
}