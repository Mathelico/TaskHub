using Microsoft.EntityFrameworkCore;
using TaskHub.Domain.Entities;

namespace TaskHub.Infrastructure.Persistence;

public sealed class ApplicationDbContext
    : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ) : base(options)
    {
    }

    public DbSet<Workspace> Workspaces =>
        Set<Workspace>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder
    )
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
    }
}