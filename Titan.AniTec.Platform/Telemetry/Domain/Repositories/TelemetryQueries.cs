namespace Titan.AniTec.Platform.Telemetry.Domain.Repositories;

public record GetReadingsQuery(int FarmId, int? DeviceId, int? AnimalId, string? MetricType,
    DateTime? StartDate, DateTime? EndDate, int? Limit);

public record GetLatestReadingQuery(int FarmId, int? DeviceId, int? AnimalId, string MetricType);

public record GetWeightsByAnimalQuery(int FarmId, int AnimalId, DateTime? StartDate, DateTime? EndDate);

public record GetLatestWeightQuery(int FarmId, int AnimalId);

public record GetWeightTrendQuery(int FarmId, int AnimalId);

public record GetWeightsSummaryQuery(int FarmId);

public record GetGpsHistoryQuery(int FarmId, int DeviceId, DateTime? StartDate, DateTime? EndDate);

public record GetThresholdsByDeviceQuery(int FarmId, int DeviceId);

public record GetTelemetryAlertsQuery(int FarmId, int? DeviceId, int? AnimalId, bool? IsActive);

public record GetTelemetryAlertByIdQuery(int AlertId);
