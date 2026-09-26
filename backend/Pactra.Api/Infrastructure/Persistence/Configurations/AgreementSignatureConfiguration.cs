using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class AgreementSignatureConfiguration
    : IEntityTypeConfiguration<AgreementSignature>
{
    public void Configure(EntityTypeBuilder<AgreementSignature> entity)
    {
        entity.HasKey(signature => signature.Id);

        entity.Property(signature => signature.SignerRole)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(signature => signature.SignedAt)
            .IsRequired();

        entity.HasOne(signature => signature.Agreement)
            .WithMany()
            .HasForeignKey(signature => signature.AgreementId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(signature => signature.Signer)
            .WithMany()
            .HasForeignKey(signature => signature.SignerId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(signature => new
        {
            signature.AgreementId,
            signature.SignerRole
        })
        .IsUnique();
    }
}