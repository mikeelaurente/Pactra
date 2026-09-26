using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> entity)
    {
        entity.HasKey(log => log.Id);

        entity.Property(log => log.Action)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(log => log.EntityType)
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(log => log.Description)
            .HasMaxLength(1000);

        entity.Property(log => log.CreatedAt)
            .IsRequired();

        entity.HasOne(log => log.Actor)
            .WithMany()
            .HasForeignKey(log => log.ActorId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(log => new
        {
            log.EntityType,
            log.EntityId
        });

        entity.HasIndex(log => log.ActorId);
    }
}