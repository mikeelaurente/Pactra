using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class SessionConfiguration
    : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> entity)
    {
        entity.HasKey(session => session.Id);

        entity.Property(session => session.RefreshTokenHash)
            .HasMaxLength(500)
            .IsRequired();

        entity.Property(session => session.CreatedAt)
            .IsRequired();

        entity.Property(session => session.ExpiresAt)
            .IsRequired();

        entity.HasOne(session => session.User)
            .WithMany()
            .HasForeignKey(session => session.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(session => session.RefreshTokenHash)
            .IsUnique();

        entity.HasIndex(session => session.UserId);
    }
}