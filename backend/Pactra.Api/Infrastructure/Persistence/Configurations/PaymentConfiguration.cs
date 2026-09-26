using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> entity)
    {
        entity.HasKey(payment => payment.Id);

        entity.Property(payment => payment.Type)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(payment => payment.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        entity.Property(payment => payment.ReferenceNumber)
            .HasMaxLength(255);

        entity.Property(payment => payment.Status)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(payment => payment.CreatedAt)
            .IsRequired();

        entity.HasOne(payment => payment.Engagement)
            .WithMany()
            .HasForeignKey(payment => payment.EngagementId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(payment => payment.Verifier)
            .WithMany()
            .HasForeignKey(payment => payment.VerifiedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}