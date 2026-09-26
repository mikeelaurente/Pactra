using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class RequirementConfiguration
    : IEntityTypeConfiguration<Requirement>
{
    public void Configure(EntityTypeBuilder<Requirement> entity)
    {
        entity.HasKey(requirement => requirement.Id);

        entity.Property(requirement => requirement.Title)
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(requirement => requirement.Description)
            .HasMaxLength(1000);

        entity.Property(requirement => requirement.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(requirement => requirement.DueDate)
            .IsRequired();

        entity.Property(requirement => requirement.CreatedAt)
            .IsRequired();

        entity.HasOne(requirement => requirement.Engagement)
            .WithMany()
            .HasForeignKey(requirement => requirement.EngagementId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(requirement => requirement.Creator)
            .WithMany()
            .HasForeignKey(requirement => requirement.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}