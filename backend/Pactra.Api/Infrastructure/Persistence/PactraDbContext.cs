using Microsoft.EntityFrameworkCore;
using Pactra.Api.Domain.Entities;

namespace Pactra.Api.Infrastructure.Persistence;

public class PactraDbContext : DbContext
{
    public PactraDbContext(DbContextOptions<PactraDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PactraDbContext).Assembly
        );
    }
}