using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;

public class TelemetryAlert : IAuditableEntity
{
    public TelemetryAlert(int farmId, int deviceId, string alertType, string severity,
        string message, int? animalId = null, string? metricType = null,
        decimal? thresholdValue = null, decimal? actualValue = null)
    {
        FarmId = farmId;
        DeviceId = deviceId;
        AlertType = alertType;
        Severity = severity;
        Message = message;
        AnimalId = animalId;
        MetricType = metricType;
        ThresholdValue = thresholdValue;
        ActualValue = actualValue;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int DeviceId { get; private set; }
    public int? AnimalId { get; private set; }
    public string AlertType { get; private set; }
    public string Severity { get; private set; }
    public string Message { get; private set; }
    public string? MetricType { get; private set; }
    public decimal? ThresholdValue { get; private set; }
    public decimal? ActualValue { get; private set; }
    public bool IsAcknowledged { get; private set; }
    public DateTime? AcknowledgedAt { get; private set; }
    public int? AcknowledgedBy { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public TelemetryAlert Acknowledge(int userId)
    {
        IsAcknowledged = true;
        AcknowledgedAt = DateTime.UtcNow;
        AcknowledgedBy = userId;
        return this;
    }
}
