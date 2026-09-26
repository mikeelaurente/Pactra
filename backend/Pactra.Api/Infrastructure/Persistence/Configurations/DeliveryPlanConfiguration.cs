using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class DeliveryPlanConfiguration
    : IEntityTypeConfiguration<DeliveryPlan>
{
    public void Configure(EntityTypeBuilder<DeliveryPlan> entity)
    {
        entity.HasKey(dp => dp.Id);

        entity.Property(dp => dp.Scope)
            .HasMaxLength(5000)
            .IsRequired();

        entity.Property(dp => dp.Deliverables)
            .HasMaxLength(5000);

        entity.Property(dp => dp.Milestones)
            .HasMaxLength(5000);

        entity.Property(dp => dp.Timeline)
            .HasMaxLength(2000);

        entity.Property(dp => dp.Assumptions)
            .HasMaxLength(5000);

        entity.Property(dp => dp.Exclusions)
            .HasMaxLength(5000);

        entity.Property(dp => dp.CreatedAt)
            .IsRequired();

        entity.HasOne(dp => dp.Engagement)
            .WithOne()
            .HasForeignKey<DeliveryPlan>(dp => dp.EngagementId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}