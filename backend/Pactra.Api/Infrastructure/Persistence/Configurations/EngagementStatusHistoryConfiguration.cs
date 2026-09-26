using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;
namespace Pactra.Api.Infrastructure.Persistence.Configurations;
public class EngagementStatusHistoryConfiguration
    : IEntityTypeConfiguration<EngagementStatusHistory>
{
    public void Configure(EntityTypeBuilder<EngagementStatusHistory> entity)
    {
        entity.HasKey(esh => esh.Id);

        entity.Property(esh => esh.FromStatus)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(esh => esh.ToStatus)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(esh => esh.ChangedAt)
            .IsRequired();

        entity.HasOne(esh => esh.Engagement)
            .WithMany()
            .HasForeignKey(esh => esh.EngagementId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(esh => esh.Actor)
            .WithMany()
            .HasForeignKey(esh => esh.ChangedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}