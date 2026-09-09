using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Domain.Entities;

namespace TaskHub.Infrastructure.Persistence.Configurations;

public sealed class WorkspaceConfiguration
    : IEntityTypeConfiguration<Workspace>
{
    public void Configure(
        EntityTypeBuilder<Workspace> builder
    )
    {
        builder.ToTable("workspaces");

        builder.HasKey(workspace => workspace.Id);

        builder.Property(workspace => workspace.Id)
            .HasColumnName("id");

        builder.Property(workspace => workspace.Name)
            .HasColumnName("name")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(workspace => workspace.OwnerId)
            .HasColumnName("owner_id")
            .IsRequired();

        builder.Property(workspace => workspace.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.HasIndex(workspace => workspace.OwnerId)
            .HasDatabaseName("ix_workspaces_owner_id");
    }
}