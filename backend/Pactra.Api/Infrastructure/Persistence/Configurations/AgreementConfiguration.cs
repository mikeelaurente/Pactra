using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class AgreementConfiguration : IEntityTypeConfiguration<Agreement>
{
    public void Configure(EntityTypeBuilder<Agreement> entity)
    {
        entity.HasKey(agreement => agreement.Id);

        entity.Property(agreement => agreement.Version)
            .IsRequired();

        entity.Property(agreement => agreement.Status)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(agreement => agreement.CreatedAt)
            .IsRequired();

        entity.HasOne(agreement => agreement.Engagement)
            .WithMany()
            .HasForeignKey(agreement => agreement.EngagementId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(agreement => agreement.EngagementId)
            .IsUnique();
    }
}