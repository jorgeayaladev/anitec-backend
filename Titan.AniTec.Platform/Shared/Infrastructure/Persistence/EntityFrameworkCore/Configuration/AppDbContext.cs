using Titan.AniTec.Platform.Financial.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Health.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Inventory.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Livestock.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Profile.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Titan.AniTec.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Device.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Telemetry.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Appointment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Communication.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Titan.AniTec.Platform.Admin.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyIamConfiguration();
        builder.ApplyProfileConfiguration();
        builder.ApplyLivestockConfiguration();
        builder.ApplyHealthConfiguration();
        builder.ApplyInventoryConfiguration();
        builder.ApplyFinancialConfiguration();
        builder.ApplySubscriptionConfiguration();
        builder.ApplyDeviceConfiguration();
        builder.ApplyTelemetryConfiguration();
        builder.ApplyAppointmentConfiguration();
        builder.ApplyAnalyticsConfiguration();
        builder.ApplyCommunicationConfiguration();
        builder.ApplyAdminConfiguration();

        builder.UseSnakeCaseNamingConvention();
    }
}
