using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities.Authentication;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        entity.HasKey(role => role.Id);

        entity.Property(role => role.Name)
            .HasMaxLength(50)
            .IsRequired();

        entity.HasIndex(role => role.Name)
            .IsUnique();

        entity.HasData(
            new Role { Id = 1, Name = "CLIENT" },
            new Role { Id = 2, Name = "PROVIDER" },
            new Role { Id = 3, Name = "OPERATIONS" },
            new Role { Id = 4, Name = "PLATFORM_ADMIN" }
        );
    }
}