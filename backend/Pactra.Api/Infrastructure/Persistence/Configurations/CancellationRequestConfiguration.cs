using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;
public class CancellationRequestConfiguration
    : IEntityTypeConfiguration<CancellationRequest>
{
    public void Configure(EntityTypeBuilder<CancellationRequest> entity)
    {
        entity.HasKey(cr => cr.Id);

        entity.Property(cr => cr.Reason)
            .HasMaxLength(1000)
            .IsRequired();

        entity.Property(cr => cr.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(cr => cr.RequestedAt)
            .IsRequired();

        entity.HasOne(cr => cr.Engagement)
            .WithMany()
            .HasForeignKey(cr => cr.EngagementId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(cr => cr.Requester)
            .WithMany()
            .HasForeignKey(cr => cr.RequestedBy)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(cr => cr.EngagementId)
            .IsUnique()
            .HasFilter("\"Status\" = 'PENDING'");
    }
}