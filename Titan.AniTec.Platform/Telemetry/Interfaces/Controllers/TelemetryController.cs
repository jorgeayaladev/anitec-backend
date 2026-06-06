using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Telemetry.Application.CommandServices;
using Titan.AniTec.Platform.Telemetry.Domain.Repositories;
using Titan.AniTec.Platform.Telemetry.Interfaces.Assemblers;
using Titan.AniTec.Platform.Telemetry.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Telemetry.Interfaces.Controllers;

[ApiController]
[Route("api/telemetry")]
[Authorize]
public class TelemetryController(
    ITelemetryQueryService queryService,
    ITelemetryCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    // --- Lecturas Generales ---
    [HttpGet("devices/{deviceId:int}/readings")]
    public async Task<IActionResult> GetDeviceReadings(int deviceId,
        [FromQuery] string? metricType, [FromQuery] int? limit)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, metricType, null, null, limit);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("devices/{deviceId:int}/readings/latest")]
    public async Task<IActionResult> GetLatestReading(int deviceId, [FromQuery] string metricType)
    {
        var query = new GetLatestReadingQuery(CurrentUserId, deviceId, null, metricType);
        var result = await queryService.GetLatestReadingAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("devices/{deviceId:int}/readings/{metricName}")]
    public async Task<IActionResult> GetMetricReadings(int deviceId, string metricName,
        [FromQuery] int? limit)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, metricName, null, null, limit);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("devices/{deviceId:int}/readings/range")]
    public async Task<IActionResult> GetReadingsByRange(int deviceId,
        [FromQuery] string? metricType, [FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, metricType, start, end, null);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpPost("readings")]
    public async Task<IActionResult> RecordReading([FromBody] RecordReadingResource resource)
    {
        var command = TelemetryAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RecordReadingAsync(command);
        return TelemetryActionResultAssembler.ToCreatedActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpPost("readings/batch")]
    public async Task<IActionResult> RecordReadingsBatch([FromBody] TelemetryReadingBatchResource resource)
    {
        var commands = resource.Readings
            .Select(r => TelemetryAssembler.ToCommand(CurrentUserId, r))
            .ToList();
        var batchCommand = new RecordReadingsBatchCommand(CurrentUserId, commands);
        var result = await commandService.RecordReadingsBatchAsync(batchCommand);
        return TelemetryActionResultAssembler.ToCreatedActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("readings/latest")]
    public async Task<IActionResult> GetAllLatestReadings()
    {
        var query = new GetReadingsQuery(CurrentUserId, null, null, null, null, null, null);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    // --- Pesos ---
    [HttpGet("weights/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetWeightsByAnimal(int animalId,
        [FromQuery] DateTime? start, [FromQuery] DateTime? end)
    {
        var query = new GetWeightsByAnimalQuery(CurrentUserId, animalId, start, end);
        var result = await queryService.GetWeightsByAnimalAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("weights/by-animal/{animalId:int}/latest")]
    public async Task<IActionResult> GetLatestWeight(int animalId)
    {
        var query = new GetLatestWeightQuery(CurrentUserId, animalId);
        var result = await queryService.GetLatestWeightAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("weights/by-animal/{animalId:int}/trend")]
    public async Task<IActionResult> GetWeightTrend(int animalId)
    {
        var query = new GetWeightTrendQuery(CurrentUserId, animalId);
        var result = await queryService.GetWeightTrendAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpPost("weights")]
    public async Task<IActionResult> RecordWeight([FromBody] RecordReadingResource resource)
    {
        var command = TelemetryAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RecordReadingAsync(command);
        return TelemetryActionResultAssembler.ToCreatedActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("weights/summary")]
    public async Task<IActionResult> GetWeightsSummary()
    {
        var query = new GetWeightsSummaryQuery(CurrentUserId);
        var result = await queryService.GetWeightsSummaryAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    // --- Collares Inteligentes ---
    [HttpGet("collars/{deviceId:int}/temperature")]
    public async Task<IActionResult> GetCollarTemperature(int deviceId)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, "temperature_body", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("collars/{deviceId:int}/activity")]
    public async Task<IActionResult> GetCollarActivity(int deviceId)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, "activity", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("collars/{deviceId:int}/feeding-time")]
    public async Task<IActionResult> GetCollarFeedingTime(int deviceId)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, "feeding_time", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("collars/{deviceId:int}/rumination")]
    public async Task<IActionResult> GetCollarRumination(int deviceId)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, "rumination", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("collars/{deviceId:int}/gps/location")]
    public async Task<IActionResult> GetCollarGpsLocation(int deviceId)
    {
        var query = new GetLatestReadingQuery(CurrentUserId, deviceId, null, "gps_location");
        var result = await queryService.GetLatestReadingAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("collars/{deviceId:int}/gps/history")]
    public async Task<IActionResult> GetCollarGpsHistory(int deviceId,
        [FromQuery] DateTime? start, [FromQuery] DateTime? end)
    {
        var query = new GetGpsHistoryQuery(CurrentUserId, deviceId, start, end);
        var result = await queryService.GetGpsHistoryAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("collars/{deviceId:int}/gps/geofence")]
    public async Task<IActionResult> GetCollarGeofenceStatus(int deviceId)
    {
        var query = new GetLatestReadingQuery(CurrentUserId, deviceId, null, "geofence_event");
        var result = await queryService.GetLatestReadingAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("collars/{deviceId:int}/health-index")]
    public async Task<IActionResult> GetCollarHealthIndex(int deviceId)
    {
        var query = new GetLatestReadingQuery(CurrentUserId, deviceId, null, "health_index");
        var result = await queryService.GetLatestReadingAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("collars/{deviceId:int}/heat-detection")]
    public async Task<IActionResult> GetCollarHeatDetection(int deviceId)
    {
        var query = new GetLatestReadingQuery(CurrentUserId, deviceId, null, "heat_detection");
        var result = await queryService.GetLatestReadingAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("collars/{deviceId:int}/early-warnings")]
    public async Task<IActionResult> GetCollarEarlyWarnings(int deviceId)
    {
        var query = new GetTelemetryAlertsQuery(CurrentUserId, deviceId, null, true);
        var result = await queryService.GetTelemetryAlertsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    // --- Aretes RFID ---
    [HttpGet("rfid/{tagId}/readings")]
    public async Task<IActionResult> GetRfidReadings(string tagId)
    {
        var query = new GetReadingsQuery(CurrentUserId, null, null, "rfid_tag", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        var filtered = result.Map(list => list
            .Where(r => r.StringValue == tagId)
            .Select(TelemetryAssembler.ToResource).ToList());
        return TelemetryActionResultAssembler.ToActionResult(filtered);
    }

    [HttpGet("rfid/{tagId}/last-location")]
    public async Task<IActionResult> GetRfidLastLocation(string tagId)
    {
        var query = new GetReadingsQuery(CurrentUserId, null, null, "rfid_tag", null, null, 1);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpPost("rfid/scan")]
    public async Task<IActionResult> RegisterRfidScan([FromBody] RecordReadingResource resource)
    {
        var command = TelemetryAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RecordReadingAsync(command);
        return TelemetryActionResultAssembler.ToCreatedActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("rfid/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetRfidHistoryByAnimal(int animalId)
    {
        var query = new GetReadingsQuery(CurrentUserId, null, animalId, "rfid_tag", null, null, null);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    // --- Cámaras Térmicas ---
    [HttpGet("thermal/{deviceId:int}/readings")]
    public async Task<IActionResult> GetThermalReadings(int deviceId)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, "thermal_temperature", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("thermal/{deviceId:int}/images")]
    public async Task<IActionResult> GetThermalImages(int deviceId)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, "thermal_image", null, null, 50);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("thermal/{deviceId:int}/images/{imageId:int}")]
    public async Task<IActionResult> GetThermalImage(int deviceId, int imageId)
    {
        var query = new GetReadingsQuery(CurrentUserId, deviceId, null, "thermal_image", null, null, null);
        var result = await queryService.GetReadingsAsync(query);
        var image = result.Map(list => list
            .Where(r => r.Id == imageId)
            .Select(TelemetryAssembler.ToResource)
            .FirstOrDefault());
        if (image.IsFailure || image.Value == null)
            return NotFound();
        return Ok(image.Value);
    }

    [HttpGet("thermal/{deviceId:int}/anomalies")]
    public async Task<IActionResult> GetThermalAnomalies(int deviceId)
    {
        var query = new GetTelemetryAlertsQuery(CurrentUserId, deviceId, null, true);
        var result = await queryService.GetTelemetryAlertsAsync(query);
        var thermalAlerts = result.Map(list => list
            .Where(a => a.AlertType.Contains("thermal", StringComparison.OrdinalIgnoreCase))
            .Select(TelemetryAssembler.ToResource).ToList());
        return TelemetryActionResultAssembler.ToActionResult(thermalAlerts);
    }

    [HttpGet("thermal/{deviceId:int}/alerts")]
    public async Task<IActionResult> GetThermalAlerts(int deviceId)
    {
        var query = new GetTelemetryAlertsQuery(CurrentUserId, deviceId, null, true);
        var result = await queryService.GetTelemetryAlertsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    // --- Estaciones Meteorológicas ---
    [HttpGet("weather/{stationId:int}/current")]
    public async Task<IActionResult> GetWeatherCurrent(int stationId)
    {
        var query = new GetLatestReadingQuery(CurrentUserId, stationId, null, "temperature_ambient");
        var result = await queryService.GetLatestReadingAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("weather/{stationId:int}/temperature")]
    public async Task<IActionResult> GetWeatherTemperature(int stationId)
    {
        var query = new GetReadingsQuery(CurrentUserId, stationId, null, "temperature_ambient", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("weather/{stationId:int}/humidity")]
    public async Task<IActionResult> GetWeatherHumidity(int stationId)
    {
        var query = new GetReadingsQuery(CurrentUserId, stationId, null, "humidity", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("weather/{stationId:int}/air-quality")]
    public async Task<IActionResult> GetWeatherAirQuality(int stationId)
    {
        var query = new GetReadingsQuery(CurrentUserId, stationId, null, "air_quality", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("weather/{stationId:int}/gases")]
    public async Task<IActionResult> GetWeatherGases(int stationId)
    {
        var query = new GetReadingsQuery(CurrentUserId, stationId, null, "gas_concentration", null, null, 100);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("weather/{stationId:int}/forecast")]
    public async Task<IActionResult> GetWeatherForecast(int stationId)
    {
        var query = new GetReadingsQuery(CurrentUserId, stationId, null, "forecast", null, null, 10);
        var result = await queryService.GetReadingsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("weather/{stationId:int}/alerts")]
    public async Task<IActionResult> GetWeatherAlerts(int stationId)
    {
        var query = new GetTelemetryAlertsQuery(CurrentUserId, stationId, null, true);
        var result = await queryService.GetTelemetryAlertsAsync(query);
        var weatherAlerts = result.Map(list => list
            .Where(a => a.AlertType.Contains("weather", StringComparison.OrdinalIgnoreCase)
                     || a.AlertType.Contains("meteorological", StringComparison.OrdinalIgnoreCase))
            .Select(TelemetryAssembler.ToResource).ToList());
        return TelemetryActionResultAssembler.ToActionResult(weatherAlerts);
    }

    // --- Umbrales ---
    [HttpGet("thresholds/{deviceId:int}")]
    public async Task<IActionResult> GetThresholdsByDevice(int deviceId)
    {
        var query = new GetThresholdsByDeviceQuery(CurrentUserId, deviceId);
        var result = await queryService.GetThresholdsByDeviceAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpPost("thresholds/{deviceId:int}")]
    public async Task<IActionResult> CreateThreshold(int deviceId, [FromBody] CreateThresholdResource resource)
    {
        var command = TelemetryAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.CreateThresholdAsync(command);
        return TelemetryActionResultAssembler.ToCreatedActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpPut("thresholds/{thresholdId:int}")]
    public async Task<IActionResult> UpdateThreshold(int thresholdId, [FromBody] UpdateThresholdResource resource)
    {
        var command = TelemetryAssembler.ToCommand(thresholdId, resource);
        var result = await commandService.UpdateThresholdAsync(command);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpDelete("thresholds/{thresholdId:int}")]
    public async Task<IActionResult> DeleteThreshold(int thresholdId)
    {
        var command = new DeleteThresholdCommand(thresholdId);
        var result = await commandService.DeleteThresholdAsync(command);
        return TelemetryActionResultAssembler.ToActionResult(result);
    }

    // --- Alertas de Telemetría ---
    [HttpGet("alerts")]
    public async Task<IActionResult> GetTelemetryAlerts([FromQuery] bool? active)
    {
        var query = new GetTelemetryAlertsQuery(CurrentUserId, null, null, active);
        var result = await queryService.GetTelemetryAlertsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("alerts/{alertId:int}")]
    public async Task<IActionResult> GetTelemetryAlertById(int alertId)
    {
        var query = new GetTelemetryAlertByIdQuery(alertId);
        var result = await queryService.GetTelemetryAlertByIdAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpPut("alerts/{alertId:int}/acknowledge")]
    public async Task<IActionResult> AcknowledgeAlert(int alertId)
    {
        var command = new AcknowledgeAlertCommand(alertId, CurrentUserId);
        var result = await commandService.AcknowledgeAlertAsync(command);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(TelemetryAssembler.ToResource));
    }

    [HttpGet("alerts/active")]
    public async Task<IActionResult> GetActiveAlerts()
    {
        var query = new GetTelemetryAlertsQuery(CurrentUserId, null, null, true);
        var result = await queryService.GetTelemetryAlertsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("alerts/by-animal/{animalId:int}")]
    public async Task<IActionResult> GetAlertsByAnimal(int animalId)
    {
        var query = new GetTelemetryAlertsQuery(CurrentUserId, null, animalId, null);
        var result = await queryService.GetTelemetryAlertsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }

    [HttpGet("alerts/by-device/{deviceId:int}")]
    public async Task<IActionResult> GetAlertsByDevice(int deviceId)
    {
        var query = new GetTelemetryAlertsQuery(CurrentUserId, deviceId, null, null);
        var result = await queryService.GetTelemetryAlertsAsync(query);
        return TelemetryActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(TelemetryAssembler.ToResource).ToList()));
    }
}
