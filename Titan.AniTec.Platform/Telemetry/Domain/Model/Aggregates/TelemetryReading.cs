using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;

public class TelemetryReading : IAuditableEntity
{
    public TelemetryReading(int farmId, string metricType, decimal? numericValue, string? stringValue,
        string? unit, DateTime recordedAt, int? deviceId = null, int? animalId = null, string? metadata = null)
    {
        FarmId = farmId;
        MetricType = metricType;
        NumericValue = numericValue;
        StringValue = stringValue;
        Unit = unit;
        RecordedAt = recordedAt;
        DeviceId = deviceId;
        AnimalId = animalId;
        Metadata = metadata;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int? DeviceId { get; private set; }
    public int? AnimalId { get; private set; }
    public string MetricType { get; private set; }
    public decimal? NumericValue { get; private set; }
    public string? StringValue { get; private set; }
    public string? Unit { get; private set; }
    public DateTime RecordedAt { get; private set; }
    public string? Metadata { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
