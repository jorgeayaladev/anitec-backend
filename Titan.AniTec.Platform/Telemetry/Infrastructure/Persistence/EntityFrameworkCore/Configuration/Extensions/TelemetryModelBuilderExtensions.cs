using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Telemetry.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class TelemetryModelBuilderExtensions
{
    public static void ApplyTelemetryConfiguration(this ModelBuilder builder)
    {
        builder.Entity<TelemetryReading>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(r => r.FarmId).IsRequired();
            entity.HasIndex(r => r.FarmId);
            entity.Property(r => r.DeviceId);
            entity.HasIndex(r => r.DeviceId);
            entity.Property(r => r.AnimalId);
            entity.HasIndex(r => r.AnimalId);
            entity.Property(r => r.MetricType).IsRequired().HasMaxLength(100);
            entity.HasIndex(r => r.MetricType);
            entity.HasIndex(r => r.RecordedAt);
            entity.Property(r => r.NumericValue).HasColumnType("decimal(18,4)");
            entity.Property(r => r.StringValue).HasMaxLength(2000);
            entity.Property(r => r.Unit).HasMaxLength(50);
            entity.Property(r => r.RecordedAt).IsRequired();
            entity.Property(r => r.Metadata).HasMaxLength(4000);
        });

        builder.Entity<TelemetryThreshold>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(t => t.FarmId).IsRequired();
            entity.HasIndex(t => t.FarmId);
            entity.Property(t => t.DeviceId).IsRequired();
            entity.HasIndex(t => t.DeviceId);
            entity.Property(t => t.MetricType).IsRequired().HasMaxLength(100);
            entity.HasIndex(t => new { t.DeviceId, t.MetricType }).IsUnique().HasFilter(null);
            entity.Property(t => t.MinValue).HasColumnType("decimal(18,4)");
            entity.Property(t => t.MaxValue).HasColumnType("decimal(18,4)");
        });

        builder.Entity<TelemetryAlert>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(a => a.FarmId).IsRequired();
            entity.HasIndex(a => a.FarmId);
            entity.Property(a => a.DeviceId).IsRequired();
            entity.HasIndex(a => a.DeviceId);
            entity.Property(a => a.AnimalId);
            entity.HasIndex(a => a.AnimalId);
            entity.Property(a => a.AlertType).IsRequired().HasMaxLength(100);
            entity.Property(a => a.Severity).IsRequired().HasMaxLength(50);
            entity.Property(a => a.Message).IsRequired().HasMaxLength(2000);
            entity.Property(a => a.MetricType).HasMaxLength(100);
            entity.Property(a => a.ThresholdValue).HasColumnType("decimal(18,4)");
            entity.Property(a => a.ActualValue).HasColumnType("decimal(18,4)");
        });
    }
}
