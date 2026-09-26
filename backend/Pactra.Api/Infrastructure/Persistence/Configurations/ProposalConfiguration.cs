using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> entity)
    {
        entity.HasKey(proposal => proposal.Id);

        entity.Property(proposal => proposal.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        entity.Property(proposal => proposal.Description)
            .HasMaxLength(1000);

        entity.Property(proposal => proposal.Terms)
            .HasMaxLength(2000);

        entity.Property(proposal => proposal.Status)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(proposal => proposal.CreatedAt)
            .IsRequired();

        entity.HasOne(proposal => proposal.Engagement)
            .WithMany()
            .HasForeignKey(proposal => proposal.EngagementId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(proposal => proposal.Proposer)
            .WithMany()
            .HasForeignKey(proposal => proposal.ProposedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}