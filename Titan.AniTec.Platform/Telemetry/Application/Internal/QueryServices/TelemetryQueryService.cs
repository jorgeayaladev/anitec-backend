using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Telemetry.Application.CommandServices;
using Titan.AniTec.Platform.Telemetry.Domain.Model;
using Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Telemetry.Domain.Repositories;

namespace Titan.AniTec.Platform.Telemetry.Application.Internal.QueryServices;

public class TelemetryQueryService(
    ITelemetryReadingRepository readingRepository,
    ITelemetryThresholdRepository thresholdRepository,
    ITelemetryAlertRepository alertRepository) : ITelemetryQueryService
{
    public async Task<Result<IReadOnlyCollection<TelemetryReading>>> GetReadingsAsync(GetReadingsQuery query)
    {
        try
        {
            IReadOnlyCollection<TelemetryReading> readings;

            if (query.DeviceId.HasValue)
                readings = await readingRepository.FindByDeviceIdAsync(query.DeviceId.Value,
                    query.MetricType, query.StartDate, query.EndDate, query.Limit);
            else if (query.AnimalId.HasValue)
                readings = await readingRepository.FindByAnimalIdAsync(query.AnimalId.Value,
                    query.MetricType, query.StartDate, query.EndDate, query.Limit);
            else
                readings = await readingRepository.FindByFarmIdAsync(query.FarmId,
                    query.MetricType, query.Limit);

            return Result<IReadOnlyCollection<TelemetryReading>>.Success(readings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<TelemetryReading>>.Failure(TelemetryError.ReadingNotFound);
        }
    }

    public async Task<Result<TelemetryReading>> GetLatestReadingAsync(GetLatestReadingQuery query)
    {
        try
        {
            TelemetryReading? reading;

            if (query.DeviceId.HasValue)
                reading = await readingRepository.FindLatestByDeviceAndMetricAsync(
                    query.DeviceId.Value, query.MetricType);
            else if (query.AnimalId.HasValue)
                reading = await readingRepository.FindLatestByAnimalAndMetricAsync(
                    query.AnimalId.Value, query.MetricType);
            else
                return Result<TelemetryReading>.Failure(TelemetryError.ReadingNotFound);

            if (reading == null)
                return Result<TelemetryReading>.Failure(TelemetryError.ReadingNotFound);

            return Result<TelemetryReading>.Success(reading);
        }
        catch (Exception)
        {
            return Result<TelemetryReading>.Failure(TelemetryError.ReadingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<TelemetryReading>>> GetWeightsByAnimalAsync(
        GetWeightsByAnimalQuery query)
    {
        try
        {
            var readings = await readingRepository.FindByAnimalIdAsync(query.AnimalId,
                "weight", query.StartDate, query.EndDate);
            return Result<IReadOnlyCollection<TelemetryReading>>.Success(readings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<TelemetryReading>>.Failure(TelemetryError.ReadingNotFound);
        }
    }

    public async Task<Result<TelemetryReading>> GetLatestWeightAsync(GetLatestWeightQuery query)
    {
        try
        {
            var reading = await readingRepository.FindLatestByAnimalAndMetricAsync(
                query.AnimalId, "weight");
            if (reading == null)
                return Result<TelemetryReading>.Failure(TelemetryError.ReadingNotFound);
            return Result<TelemetryReading>.Success(reading);
        }
        catch (Exception)
        {
            return Result<TelemetryReading>.Failure(TelemetryError.ReadingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<TelemetryReading>>> GetWeightTrendAsync(
        GetWeightTrendQuery query)
    {
        try
        {
            var readings = await readingRepository.FindByAnimalIdAsync(query.AnimalId,
                "weight", null, null, 100);
            return Result<IReadOnlyCollection<TelemetryReading>>.Success(readings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<TelemetryReading>>.Failure(TelemetryError.ReadingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<TelemetryReading>>> GetWeightsSummaryAsync(
        GetWeightsSummaryQuery query)
    {
        try
        {
            var readings = await readingRepository.FindByFarmIdAsync(query.FarmId, "weight", 1000);
            return Result<IReadOnlyCollection<TelemetryReading>>.Success(readings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<TelemetryReading>>.Failure(TelemetryError.ReadingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<TelemetryReading>>> GetGpsHistoryAsync(
        GetGpsHistoryQuery query)
    {
        try
        {
            var readings = await readingRepository.FindByDeviceIdAsync(query.DeviceId,
                "gps_location", query.StartDate, query.EndDate);
            return Result<IReadOnlyCollection<TelemetryReading>>.Success(readings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<TelemetryReading>>.Failure(TelemetryError.ReadingNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<TelemetryThreshold>>> GetThresholdsByDeviceAsync(
        GetThresholdsByDeviceQuery query)
    {
        try
        {
            var thresholds = await thresholdRepository.FindByDeviceIdAsync(query.DeviceId);
            return Result<IReadOnlyCollection<TelemetryThreshold>>.Success(thresholds);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<TelemetryThreshold>>.Failure(TelemetryError.ThresholdNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<TelemetryAlert>>> GetTelemetryAlertsAsync(
        GetTelemetryAlertsQuery query)
    {
        try
        {
            IReadOnlyCollection<TelemetryAlert> alerts;

            if (query.DeviceId.HasValue)
                alerts = await alertRepository.FindByDeviceIdAsync(query.DeviceId.Value, query.IsActive);
            else if (query.AnimalId.HasValue)
                alerts = await alertRepository.FindByAnimalIdAsync(query.AnimalId.Value, query.IsActive);
            else if (query.IsActive.HasValue && query.IsActive.Value)
                alerts = await alertRepository.FindActiveByFarmIdAsync(query.FarmId);
            else
                alerts = await alertRepository.FindByFarmIdAsync(query.FarmId, query.IsActive);

            return Result<IReadOnlyCollection<TelemetryAlert>>.Success(alerts);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<TelemetryAlert>>.Failure(TelemetryError.TelemetryAlertNotFound);
        }
    }

    public async Task<Result<TelemetryAlert>> GetTelemetryAlertByIdAsync(GetTelemetryAlertByIdQuery query)
    {
        try
        {
            var alert = await alertRepository.FindByIdAsync(query.AlertId);
            if (alert == null)
                return Result<TelemetryAlert>.Failure(TelemetryError.TelemetryAlertNotFound);
            return Result<TelemetryAlert>.Success(alert);
        }
        catch (Exception)
        {
            return Result<TelemetryAlert>.Failure(TelemetryError.TelemetryAlertNotFound);
        }
    }
}
