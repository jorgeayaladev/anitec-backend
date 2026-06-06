namespace Titan.AniTec.Platform.Telemetry.Interfaces.Resources;

public record TelemetryReadingResource(
    int Id, int FarmId, int? DeviceId, int? AnimalId, string MetricType,
    decimal? NumericValue, string? StringValue, string? Unit,
    DateTime RecordedAt, string? Metadata);

public record RecordReadingResource(
    int? DeviceId, int? AnimalId, string MetricType, decimal? NumericValue,
    string? StringValue, string? Unit, DateTime RecordedAt, string? Metadata);

public record TelemetryReadingBatchResource(IReadOnlyCollection<RecordReadingResource> Readings);

public record TelemetryThresholdResource(
    int Id, int FarmId, int DeviceId, string MetricType,
    decimal? MinValue, decimal? MaxValue, bool IsEnabled);

public record CreateThresholdResource(
    int DeviceId, string MetricType, decimal? MinValue, decimal? MaxValue, bool IsEnabled = true);

public record UpdateThresholdResource(
    decimal? MinValue, decimal? MaxValue, bool IsEnabled);

public record TelemetryAlertResource(
    int Id, int FarmId, int DeviceId, int? AnimalId, string AlertType,
    string Severity, string Message, string? MetricType, decimal? ThresholdValue,
    decimal? ActualValue, bool IsAcknowledged, DateTime? AcknowledgedAt, int? AcknowledgedBy);
