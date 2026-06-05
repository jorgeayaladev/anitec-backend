using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
            entity.HasIndex(u => u.Username).IsUnique();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Role).IsRequired().HasMaxLength(30);
            entity.Property(u => u.IsActive).IsRequired();
            entity.Property(u => u.EmailVerified).IsRequired();
            entity.Property(u => u.CreatedAt);
            entity.Property(u => u.UpdatedAt);
        });
    }
}
