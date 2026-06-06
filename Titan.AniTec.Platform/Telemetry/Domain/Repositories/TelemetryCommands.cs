using Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Telemetry.Domain.Repositories;

public record RecordReadingCommand(int FarmId, int? DeviceId, int? AnimalId, string MetricType,
    decimal? NumericValue, string? StringValue, string? Unit, DateTime RecordedAt, string? Metadata);

public record RecordReadingsBatchCommand(int FarmId, IReadOnlyCollection<RecordReadingCommand> Readings);

public record CreateThresholdCommand(int FarmId, int DeviceId, string MetricType,
    decimal? MinValue, decimal? MaxValue, bool IsEnabled = true);

public record UpdateThresholdCommand(int ThresholdId, decimal? MinValue, decimal? MaxValue, bool IsEnabled);

public record DeleteThresholdCommand(int ThresholdId);

public record AcknowledgeAlertCommand(int AlertId, int UserId);
