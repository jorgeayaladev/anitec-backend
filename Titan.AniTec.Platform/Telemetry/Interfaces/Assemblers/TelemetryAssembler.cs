using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Telemetry.Domain.Model;
using Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Telemetry.Domain.Repositories;
using Titan.AniTec.Platform.Telemetry.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Telemetry.Interfaces.Assemblers;

public static class TelemetryAssembler
{
    public static TelemetryReadingResource ToResource(TelemetryReading reading)
        => new(reading.Id, reading.FarmId, reading.DeviceId, reading.AnimalId, reading.MetricType,
            reading.NumericValue, reading.StringValue, reading.Unit, reading.RecordedAt, reading.Metadata);

    public static RecordReadingCommand ToCommand(int farmId, RecordReadingResource resource)
        => new(farmId, resource.DeviceId, resource.AnimalId, resource.MetricType,
            resource.NumericValue, resource.StringValue, resource.Unit,
            resource.RecordedAt, resource.Metadata);

    public static TelemetryThresholdResource ToResource(TelemetryThreshold threshold)
        => new(threshold.Id, threshold.FarmId, threshold.DeviceId, threshold.MetricType,
            threshold.MinValue, threshold.MaxValue, threshold.IsEnabled);

    public static CreateThresholdCommand ToCommand(int farmId, CreateThresholdResource resource)
        => new(farmId, resource.DeviceId, resource.MetricType,
            resource.MinValue, resource.MaxValue, resource.IsEnabled);

    public static UpdateThresholdCommand ToCommand(int thresholdId, UpdateThresholdResource resource)
        => new(thresholdId, resource.MinValue, resource.MaxValue, resource.IsEnabled);

    public static TelemetryAlertResource ToResource(TelemetryAlert alert)
        => new(alert.Id, alert.FarmId, alert.DeviceId, alert.AnimalId, alert.AlertType,
            alert.Severity, alert.Message, alert.MetricType, alert.ThresholdValue,
            alert.ActualValue, alert.IsAcknowledged, alert.AcknowledgedAt, alert.AcknowledgedBy);
}

public static class TelemetryActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsFailure)
        {
            return result.Error switch
            {
                TelemetryError.ReadingNotFound or
                TelemetryError.ThresholdNotFound or
                TelemetryError.TelemetryAlertNotFound => new NotFoundObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                TelemetryError.TelemetryAlertAlreadyAcknowledged => new ConflictObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                _ => new BadRequestObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() })
            };
        }
        return new OkObjectResult(result.Value);
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsFailure)
            return ToActionResult(result);
        return new CreatedResult(string.Empty, result.Value);
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsFailure)
        {
            return result.Error switch
            {
                TelemetryError.ThresholdNotFound or
                TelemetryError.TelemetryAlertNotFound => new NotFoundObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                _ => new BadRequestObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() })
            };
        }
        return new OkResult();
    }
}
