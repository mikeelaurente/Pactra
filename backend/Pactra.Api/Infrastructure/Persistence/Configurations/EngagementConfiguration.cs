using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class EngagementConfiguration
    : IEntityTypeConfiguration<Engagement>
{
    public void Configure(EntityTypeBuilder<Engagement> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(1000);

        entity.Property(e => e.Goals)
            .IsRequired()
            .HasMaxLength(1000);

        entity.Property(e => e.RequestedFeatures)
            .IsRequired()
            .HasMaxLength(1000);

        entity.Property(e => e.Constraints)
            .IsRequired()
            .HasMaxLength(1000);

        entity.Property(e => e.Budget)
            .HasPrecision(18, 2);

        entity.Property(e => e.AdditionalInfo)
            .HasMaxLength(1000);

        entity.Property(e => e.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(e => e.CreatedAt)
            .IsRequired();

        entity.Property(e => e.UpdatedAt)
            .IsRequired();

        entity.HasOne(e => e.Service)
            .WithMany()
             .HasForeignKey(e => new
            {
                e.ServiceId,
                e.ProviderId
            })
            .HasPrincipalKey(service => new
            {
                service.Id,
                service.ProviderId
            })
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Client)
            .WithMany()
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Provider)
            .WithMany()
            .HasForeignKey(e => e.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}