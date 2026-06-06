using Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Admin.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class AdminModelBuilderExtensions
{
    public static void ApplyAdminConfiguration(this ModelBuilder builder)
    {
        builder.Entity<SystemSetting>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(s => s.Key).IsRequired().HasMaxLength(200);
            entity.HasIndex(s => s.Key).IsUnique();
            entity.Property(s => s.Value).IsRequired().HasMaxLength(4000);
            entity.Property(s => s.Category).IsRequired().HasMaxLength(100);
            entity.HasIndex(s => s.Category);
        });

        builder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(l => l.UserId).IsRequired();
            entity.HasIndex(l => l.UserId);
            entity.Property(l => l.Action).IsRequired().HasMaxLength(200);
            entity.Property(l => l.EntityType).IsRequired().HasMaxLength(100);
            entity.HasIndex(l => new { l.EntityType, l.EntityId });
            entity.Property(l => l.Details).HasMaxLength(4000);
        });

        builder.Entity<ContentPage>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(p => p.Slug).IsRequired().HasMaxLength(200);
            entity.HasIndex(p => p.Slug).IsUnique();
            entity.Property(p => p.Title).IsRequired().HasMaxLength(500);
            entity.Property(p => p.Content).IsRequired().HasMaxLength(10000);
        });

        builder.Entity<Faq>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(f => f.Question).IsRequired().HasMaxLength(1000);
            entity.Property(f => f.Answer).IsRequired().HasMaxLength(10000);
        });

        builder.Entity<Announcement>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(a => a.Title).IsRequired().HasMaxLength(500);
            entity.Property(a => a.Content).IsRequired().HasMaxLength(5000);
            entity.Property(a => a.Severity).IsRequired().HasMaxLength(20);
        });
    }
}
