using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Telemetry.Domain.Repositories;

namespace Titan.AniTec.Platform.Telemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TelemetryReadingRepository(AppDbContext context)
    : BaseRepository<TelemetryReading>(context), ITelemetryReadingRepository
{
    public async Task<IReadOnlyCollection<TelemetryReading>> FindByDeviceIdAsync(int deviceId,
        string? metricType = null, DateTime? startDate = null, DateTime? endDate = null, int? limit = null)
    {
        var query = Context.Set<TelemetryReading>()
            .Where(r => r.DeviceId == deviceId);

        if (!string.IsNullOrEmpty(metricType))
            query = query.Where(r => r.MetricType == metricType);
        if (startDate.HasValue)
            query = query.Where(r => r.RecordedAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(r => r.RecordedAt <= endDate.Value);

        query = query.OrderByDescending(r => r.RecordedAt);

        if (limit.HasValue)
            query = query.Take(limit.Value);

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyCollection<TelemetryReading>> FindByAnimalIdAsync(int animalId,
        string? metricType = null, DateTime? startDate = null, DateTime? endDate = null, int? limit = null)
    {
        var query = Context.Set<TelemetryReading>()
            .Where(r => r.AnimalId == animalId);

        if (!string.IsNullOrEmpty(metricType))
            query = query.Where(r => r.MetricType == metricType);
        if (startDate.HasValue)
            query = query.Where(r => r.RecordedAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(r => r.RecordedAt <= endDate.Value);

        query = query.OrderByDescending(r => r.RecordedAt);

        if (limit.HasValue)
            query = query.Take(limit.Value);

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyCollection<TelemetryReading>> FindByFarmIdAsync(int farmId,
        string? metricType = null, int? limit = null)
    {
        var query = Context.Set<TelemetryReading>()
            .Where(r => r.FarmId == farmId);

        if (!string.IsNullOrEmpty(metricType))
            query = query.Where(r => r.MetricType == metricType);

        query = query.OrderByDescending(r => r.RecordedAt);

        if (limit.HasValue)
            query = query.Take(limit.Value);

        return await query.ToListAsync();
    }

    public async Task<TelemetryReading?> FindLatestByDeviceAndMetricAsync(int deviceId, string metricType)
        => await Context.Set<TelemetryReading>()
            .Where(r => r.DeviceId == deviceId && r.MetricType == metricType)
            .OrderByDescending(r => r.RecordedAt)
            .FirstOrDefaultAsync();

    public async Task<TelemetryReading?> FindLatestByAnimalAndMetricAsync(int animalId, string metricType)
        => await Context.Set<TelemetryReading>()
            .Where(r => r.AnimalId == animalId && r.MetricType == metricType)
            .OrderByDescending(r => r.RecordedAt)
            .FirstOrDefaultAsync();

    public async Task<IReadOnlyCollection<TelemetryReading>> FindLatestByFarmAsync(int farmId)
        => await Context.Set<TelemetryReading>()
            .Where(r => r.FarmId == farmId)
            .GroupBy(r => new { r.DeviceId, r.MetricType })
            .Select(g => g.OrderByDescending(r => r.RecordedAt).First())
            .ToListAsync();

    public async Task AddRangeAsync(IEnumerable<TelemetryReading> readings)
    {
        await Context.Set<TelemetryReading>().AddRangeAsync(readings);
    }
}

public class TelemetryThresholdRepository(AppDbContext context)
    : BaseRepository<TelemetryThreshold>(context), ITelemetryThresholdRepository
{
    public async Task<IReadOnlyCollection<TelemetryThreshold>> FindByDeviceIdAsync(int deviceId)
        => await Context.Set<TelemetryThreshold>()
            .Where(t => t.DeviceId == deviceId && t.IsEnabled)
            .ToListAsync();

    public async Task<TelemetryThreshold?> FindByDeviceAndMetricAsync(int deviceId, string metricType)
        => await Context.Set<TelemetryThreshold>()
            .FirstOrDefaultAsync(t => t.DeviceId == deviceId && t.MetricType == metricType);
}

public class TelemetryAlertRepository(AppDbContext context)
    : BaseRepository<TelemetryAlert>(context), ITelemetryAlertRepository
{
    public async Task<IReadOnlyCollection<TelemetryAlert>> FindByFarmIdAsync(int farmId, bool? isActive = null)
    {
        var query = Context.Set<TelemetryAlert>()
            .Where(a => a.FarmId == farmId);

        if (isActive.HasValue)
            query = query.Where(a => a.IsAcknowledged != isActive.Value);

        return await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<IReadOnlyCollection<TelemetryAlert>> FindByDeviceIdAsync(int deviceId, bool? isActive = null)
    {
        var query = Context.Set<TelemetryAlert>()
            .Where(a => a.DeviceId == deviceId);

        if (isActive.HasValue)
            query = query.Where(a => a.IsAcknowledged != isActive.Value);

        return await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<IReadOnlyCollection<TelemetryAlert>> FindByAnimalIdAsync(int animalId, bool? isActive = null)
    {
        var query = Context.Set<TelemetryAlert>()
            .Where(a => a.AnimalId == animalId);

        if (isActive.HasValue)
            query = query.Where(a => a.IsAcknowledged != isActive.Value);

        return await query.OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<IReadOnlyCollection<TelemetryAlert>> FindActiveByFarmIdAsync(int farmId)
        => await Context.Set<TelemetryAlert>()
            .Where(a => a.FarmId == farmId && !a.IsAcknowledged)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
}
