using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

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
    }
}