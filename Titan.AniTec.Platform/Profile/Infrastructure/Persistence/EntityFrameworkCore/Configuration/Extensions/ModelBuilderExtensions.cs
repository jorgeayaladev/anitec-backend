using Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Profile.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfileConfiguration(this ModelBuilder builder)
    {
        builder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(u => u.UserId).IsRequired();
            entity.HasIndex(u => u.UserId).IsUnique();
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Phone).HasMaxLength(20);
            entity.Property(u => u.AvatarUrl).HasMaxLength(500);
            entity.Property(u => u.Bio).HasMaxLength(1000);
            entity.Property(u => u.CreatedAt);
            entity.Property(u => u.UpdatedAt);
        });

        builder.Entity<Farm>(entity =>
        {
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(f => f.UserId).IsRequired();
            entity.HasIndex(f => f.UserId).IsUnique();
            entity.Property(f => f.FarmName).IsRequired().HasMaxLength(200);
            entity.Property(f => f.Address).HasMaxLength(500);
            entity.Property(f => f.City).HasMaxLength(100);
            entity.Property(f => f.State).HasMaxLength(100);
            entity.Property(f => f.Country).HasMaxLength(100);
            entity.Property(f => f.PostalCode).HasMaxLength(20);
            entity.Property(f => f.Description).HasMaxLength(2000);
            entity.Property(f => f.CreatedAt);
            entity.Property(f => f.UpdatedAt);

            entity.HasMany(f => f.Locations)
                .WithOne()
                .HasForeignKey(l => l.FarmId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(f => f.Staff)
                .WithOne()
                .HasForeignKey(s => s.FarmId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(f => f.Certifications)
                .WithOne()
                .HasForeignKey(c => c.FarmId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<FarmLocation>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(l => l.Name).IsRequired().HasMaxLength(200);
            entity.Property(l => l.Description).HasMaxLength(1000);
            entity.Property(l => l.CreatedAt);
            entity.Property(l => l.UpdatedAt);
        });

        builder.Entity<FarmStaff>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(s => s.FullName).IsRequired().HasMaxLength(200);
            entity.Property(s => s.Role).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Phone).HasMaxLength(20);
            entity.Property(s => s.Email).HasMaxLength(200);
            entity.Property(s => s.CreatedAt);
            entity.Property(s => s.UpdatedAt);
        });

        builder.Entity<FarmCertification>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entity.Property(c => c.IssuingAuthority).IsRequired().HasMaxLength(200);
            entity.Property(c => c.CertificationNumber).HasMaxLength(100);
            entity.Property(c => c.CreatedAt);
            entity.Property(c => c.UpdatedAt);
        });

        builder.Entity<Clinic>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(c => c.UserId).IsRequired();
            entity.HasIndex(c => c.UserId).IsUnique();
            entity.Property(c => c.ClinicName).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Address).HasMaxLength(500);
            entity.Property(c => c.City).HasMaxLength(100);
            entity.Property(c => c.State).HasMaxLength(100);
            entity.Property(c => c.Country).HasMaxLength(100);
            entity.Property(c => c.PostalCode).HasMaxLength(20);
            entity.Property(c => c.Phone).HasMaxLength(20);
            entity.Property(c => c.Email).HasMaxLength(200);
            entity.Property(c => c.Description).HasMaxLength(2000);
            entity.Property(c => c.CreatedAt);
            entity.Property(c => c.UpdatedAt);

            entity.HasMany(c => c.Schedules)
                .WithOne()
                .HasForeignKey(s => s.ClinicId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(c => c.Specialties)
                .WithOne()
                .HasForeignKey(s => s.ClinicId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(c => c.ServiceAreaLocations)
                .WithOne()
                .HasForeignKey(l => l.ClinicId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(c => c.Availabilities)
                .WithOne()
                .HasForeignKey(a => a.ClinicId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ClinicSchedule>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(s => s.OpenTime).IsRequired();
            entity.Property(s => s.CloseTime).IsRequired();
        });

        builder.Entity<Specialty>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(s => s.Name).IsRequired().HasMaxLength(200);
            entity.Property(s => s.Description).HasMaxLength(1000);
        });

        builder.Entity<ServiceAreaLocation>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(l => l.Name).IsRequired().HasMaxLength(200);
            entity.Property(l => l.Address).HasMaxLength(500);
        });

        builder.Entity<ClinicAvailability>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(a => a.StartTime).IsRequired();
            entity.Property(a => a.EndTime).IsRequired();
        });

        builder.Entity<ProfessionalLicense>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(l => l.LicenseNumber).IsRequired().HasMaxLength(100);
            entity.HasIndex(l => l.LicenseNumber).IsUnique();
            entity.Property(l => l.IssuingAuthority).IsRequired().HasMaxLength(200);
            entity.Property(l => l.CreatedAt);
            entity.Property(l => l.UpdatedAt);
        });

        builder.Entity<UserPreferences>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(p => p.UserId).IsRequired();
            entity.HasIndex(p => p.UserId).IsUnique();
            entity.Property(p => p.Language).IsRequired().HasMaxLength(10);
            entity.Property(p => p.Theme).IsRequired().HasMaxLength(20);
            entity.Property(p => p.CreatedAt);
            entity.Property(p => p.UpdatedAt);
        });

        builder.Entity<Notification>(entity =>
        {
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(n => n.UserId).IsRequired();
            entity.HasIndex(n => n.UserId);
            entity.Property(n => n.Type).IsRequired().HasMaxLength(50);
            entity.Property(n => n.Title).IsRequired().HasMaxLength(200);
            entity.Property(n => n.Message).IsRequired().HasMaxLength(2000);
            entity.Property(n => n.CreatedAt);
            entity.Property(n => n.UpdatedAt);
        });
    }
}
