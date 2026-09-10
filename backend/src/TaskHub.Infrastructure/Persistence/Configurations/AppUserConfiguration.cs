using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Infrastructure.Identity;

namespace TaskHub.Infrastructure.Persistence.Configurations;

public sealed class AppUserConfiguration
    : IEntityTypeConfiguration<AppUser>
{
    public void Configure(
        EntityTypeBuilder<AppUser> builder
    )
    {
        builder.Property(user => user.DisplayName)
            .HasMaxLength(100)
            .IsRequired();
    }
}