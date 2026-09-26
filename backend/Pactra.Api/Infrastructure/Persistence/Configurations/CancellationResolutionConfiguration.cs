using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;
namespace Pactra.Api.Infrastructure.Persistence.Configurations;
public class CancellationResolutionConfiguration
    : IEntityTypeConfiguration<CancellationResolution>
{
    public void Configure(EntityTypeBuilder<CancellationResolution> entity)
    {
        entity.HasKey(cr => cr.Id);

        entity.Property(cr => cr.Reason)
            .HasMaxLength(1000)
            .IsRequired();

        entity.Property(cr => cr.ResolvedAt)
            .IsRequired();

        entity.HasOne(cr => cr.CancellationRequest)
            .WithOne()
            .HasForeignKey<CancellationResolution>(
                cr => cr.CancellationRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(cr => cr.Resolver)
            .WithMany()
            .HasForeignKey(cr => cr.ResolvedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}