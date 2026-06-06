using Titan.AniTec.Platform.Analytics.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class AnalyticsModelBuilderExtensions
{
    public static void ApplyAnalyticsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ScheduledReport>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(r => r.FarmId).IsRequired();
            entity.HasIndex(r => r.FarmId);
            entity.Property(r => r.ReportType).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Format).IsRequired().HasMaxLength(20);
            entity.Property(r => r.Schedule).IsRequired().HasMaxLength(200);
        });
    }
}
