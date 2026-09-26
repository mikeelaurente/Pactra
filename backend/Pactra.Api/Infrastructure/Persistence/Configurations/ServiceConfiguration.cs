using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> entity)
    {
        entity.HasKey(service => service.Id);

        entity.Property(service => service.Name)
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(service => service.Description)
            .IsRequired();

        entity.Property(service => service.Status)
            .HasMaxLength(50)
            .IsRequired();

        entity.Property(service => service.CreatedAt)
            .IsRequired();

        entity.Property(service => service.UpdatedAt)
            .IsRequired();

        entity.HasOne(service => service.Provider)
            .WithMany()
            .HasForeignKey(service => service.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasAlternateKey(service => new
        {
            service.Id,
            service.ProviderId
        });
    }
}