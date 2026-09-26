using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;
namespace Pactra.Api.Infrastructure.Persistence.Configurations;
public class EngagementActivityConfiguration
    : IEntityTypeConfiguration<EngagementActivity>
{
    public void Configure(EntityTypeBuilder<EngagementActivity> entity)
    {
        entity.HasKey(ea => ea.Id);

        entity.Property(ea => ea.Type)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(ea => ea.Description)
            .HasMaxLength(1000);

        entity.Property(ea => ea.CreatedAt)
            .IsRequired();

        entity.HasOne(ea => ea.Engagement)
            .WithMany()
            .HasForeignKey(ea => ea.EngagementId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(ea => ea.Actor)
            .WithMany()
            .HasForeignKey(ea => ea.ActorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}