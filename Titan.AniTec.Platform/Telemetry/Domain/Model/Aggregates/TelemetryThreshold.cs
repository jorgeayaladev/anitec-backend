using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;

public class TelemetryThreshold : IAuditableEntity
{
    public TelemetryThreshold(int farmId, int deviceId, string metricType,
        decimal? minValue, decimal? maxValue, bool isEnabled = true)
    {
        FarmId = farmId;
        DeviceId = deviceId;
        MetricType = metricType;
        MinValue = minValue;
        MaxValue = maxValue;
        IsEnabled = isEnabled;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int DeviceId { get; private set; }
    public string MetricType { get; private set; }
    public decimal? MinValue { get; private set; }
    public decimal? MaxValue { get; private set; }
    public bool IsEnabled { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public TelemetryThreshold Update(decimal? minValue, decimal? maxValue, bool isEnabled)
    {
        MinValue = minValue;
        MaxValue = maxValue;
        IsEnabled = isEnabled;
        return this;
    }
}
