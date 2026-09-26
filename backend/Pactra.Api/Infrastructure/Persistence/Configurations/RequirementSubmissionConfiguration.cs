using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;


public class RequirementSubmissionConfiguration : IEntityTypeConfiguration<RequirementSubmission>
{
    public void Configure(EntityTypeBuilder<RequirementSubmission> entity)
    {
        entity.HasKey(submission => submission.Id);

        entity.Property(submission => submission.FileUrl)
            .HasMaxLength(1000)
            .IsRequired();

        entity.Property(submission => submission.Notes)
            .HasMaxLength(1000);

        entity.Property(submission => submission.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(submission => submission.SubmittedAt)
            .IsRequired();

        entity.HasOne(submission => submission.Requirement)
            .WithMany()
            .HasForeignKey(submission => submission.RequirementId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(submission => submission.Submitter)
            .WithMany()
            .HasForeignKey(submission => submission.SubmittedBy)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(submission => submission.Reviewer)
            .WithMany()
            .HasForeignKey(submission => submission.ReviewedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}