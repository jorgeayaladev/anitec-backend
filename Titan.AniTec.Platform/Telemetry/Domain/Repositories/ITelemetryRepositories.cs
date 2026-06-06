using Titan.AniTec.Platform.Shared.Domain.Repositories;
using Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Telemetry.Domain.Repositories;

public interface ITelemetryReadingRepository : IBaseRepository<TelemetryReading>
{
    Task<IReadOnlyCollection<TelemetryReading>> FindByDeviceIdAsync(int deviceId, string? metricType = null,
        DateTime? startDate = null, DateTime? endDate = null, int? limit = null);
    Task<IReadOnlyCollection<TelemetryReading>> FindByAnimalIdAsync(int animalId, string? metricType = null,
        DateTime? startDate = null, DateTime? endDate = null, int? limit = null);
    Task<IReadOnlyCollection<TelemetryReading>> FindByFarmIdAsync(int farmId, string? metricType = null,
        int? limit = null);
    Task<TelemetryReading?> FindLatestByDeviceAndMetricAsync(int deviceId, string metricType);
    Task<TelemetryReading?> FindLatestByAnimalAndMetricAsync(int animalId, string metricType);
    Task<IReadOnlyCollection<TelemetryReading>> FindLatestByFarmAsync(int farmId);
    Task AddRangeAsync(IEnumerable<TelemetryReading> readings);
}

public interface ITelemetryThresholdRepository : IBaseRepository<TelemetryThreshold>
{
    Task<IReadOnlyCollection<TelemetryThreshold>> FindByDeviceIdAsync(int deviceId);
    Task<TelemetryThreshold?> FindByDeviceAndMetricAsync(int deviceId, string metricType);
}

public interface ITelemetryAlertRepository : IBaseRepository<TelemetryAlert>
{
    Task<IReadOnlyCollection<TelemetryAlert>> FindByFarmIdAsync(int farmId, bool? isActive = null);
    Task<IReadOnlyCollection<TelemetryAlert>> FindByDeviceIdAsync(int deviceId, bool? isActive = null);
    Task<IReadOnlyCollection<TelemetryAlert>> FindByAnimalIdAsync(int animalId, bool? isActive = null);
    Task<IReadOnlyCollection<TelemetryAlert>> FindActiveByFarmIdAsync(int farmId);
}
