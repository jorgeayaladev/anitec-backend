namespace Titan.AniTec.Platform.Telemetry.Domain.Model;

public enum TelemetryError
{
    ReadingNotFound,
    InvalidReadingData,
    ThresholdNotFound,
    InvalidThresholdData,
    TelemetryAlertNotFound,
    TelemetryAlertAlreadyAcknowledged
}
