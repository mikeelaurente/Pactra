using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
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
    }
}