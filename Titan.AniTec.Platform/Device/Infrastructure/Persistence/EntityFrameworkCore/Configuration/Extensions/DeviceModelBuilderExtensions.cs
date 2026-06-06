using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Device.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Device.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class DeviceModelBuilderExtensions
{
    public static void ApplyDeviceConfiguration(this ModelBuilder builder)
    {
        builder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(t => t.Name).IsRequired().HasMaxLength(200);
            entity.HasIndex(t => t.Name).IsUnique();
            entity.Property(t => t.Description).HasMaxLength(2000);
            entity.Property(t => t.Category).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Specifications).HasMaxLength(4000);
            entity.Property(t => t.Metrics).HasMaxLength(4000);
        });

        builder.Entity<FarmDevice>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(d => d.FarmId).IsRequired();
            entity.HasIndex(d => d.FarmId);
            entity.Property(d => d.DeviceTypeId).IsRequired();
            entity.Property(d => d.Name).IsRequired().HasMaxLength(200);
            entity.Property(d => d.SerialNumber).IsRequired().HasMaxLength(200);
            entity.HasIndex(d => d.SerialNumber);
            entity.Property(d => d.Status).IsRequired().HasMaxLength(50);
            entity.HasIndex(d => d.Status);
            entity.Property(d => d.FirmwareVersion).HasMaxLength(100);
            entity.Property(d => d.Configuration).HasMaxLength(4000);
        });

        builder.Entity<DeviceAssignment>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(a => a.DeviceId).IsRequired();
            entity.Property(a => a.AnimalId).IsRequired();
            entity.Property(a => a.FarmId).IsRequired();
            entity.HasIndex(a => a.DeviceId);
            entity.HasIndex(a => a.AnimalId);
            entity.HasIndex(a => a.FarmId);
        });

        builder.Entity<DeviceAlert>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(a => a.DeviceId).IsRequired();
            entity.Property(a => a.FarmId).IsRequired();
            entity.HasIndex(a => a.FarmId);
            entity.Property(a => a.AlertType).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Description).IsRequired().HasMaxLength(1000);
        });
    }
}
