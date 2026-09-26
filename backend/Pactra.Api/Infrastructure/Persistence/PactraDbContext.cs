using Microsoft.EntityFrameworkCore;
using Pactra.Api.Domain.Entities;
using Pactra.Api.Infrastructure.Persistence.Configurations;

namespace Pactra.Api.Infrastructure.Persistence;

public class PactraDbContext : DbContext
{
    public PactraDbContext(DbContextOptions<PactraDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Engagement> Engagements => Set<Engagement>();
    public DbSet<Proposal> Proposals => Set<Proposal>();
    public DbSet<Agreement> Agreements => Set<Agreement>();
    public DbSet<AgreementSignature> AgreementSignatures => Set<AgreementSignature>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Requirement> Requirements => Set<Requirement>();
    public DbSet<RequirementSubmission> RequirementSubmissions => Set<RequirementSubmission>();
    public DbSet<EngagementActivity> EngagementActivities => Set<EngagementActivity>();
    public DbSet<EngagementStatusHistory> EngagementStatusHistories => Set<EngagementStatusHistory>();
    public DbSet<CancellationRequest> CancellationRequests => Set<CancellationRequest>();
    public DbSet<CancellationResolution> CancellationResolutions => Set<CancellationResolution>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PactraDbContext).Assembly
        );
    }
}