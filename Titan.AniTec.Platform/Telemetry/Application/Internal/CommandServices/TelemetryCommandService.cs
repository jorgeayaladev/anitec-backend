using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;
using Titan.AniTec.Platform.Telemetry.Application.CommandServices;
using Titan.AniTec.Platform.Telemetry.Domain.Model;
using Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Telemetry.Domain.Repositories;

namespace Titan.AniTec.Platform.Telemetry.Application.Internal.CommandServices;

public class TelemetryCommandService(
    ITelemetryReadingRepository readingRepository,
    ITelemetryThresholdRepository thresholdRepository,
    ITelemetryAlertRepository alertRepository,
    IUnitOfWork unitOfWork) : ITelemetryCommandService
{
    public async Task<Result<TelemetryReading>> RecordReadingAsync(RecordReadingCommand command)
    {
        try
        {
            var reading = new TelemetryReading(command.FarmId, command.MetricType, command.NumericValue,
                command.StringValue, command.Unit, command.RecordedAt, command.DeviceId,
                command.AnimalId, command.Metadata);
            await readingRepository.AddAsync(reading);
            await unitOfWork.CompleteAsync();
            return Result<TelemetryReading>.Success(reading);
        }
        catch (Exception)
        {
            return Result<TelemetryReading>.Failure(TelemetryError.InvalidReadingData);
        }
    }

    public async Task<Result<IReadOnlyCollection<TelemetryReading>>> RecordReadingsBatchAsync(
        RecordReadingsBatchCommand command)
    {
        try
        {
            var readings = command.Readings.Select(r => new TelemetryReading(
                command.FarmId, r.MetricType, r.NumericValue, r.StringValue, r.Unit,
                r.RecordedAt, r.DeviceId, r.AnimalId, r.Metadata)).ToList();
            await readingRepository.AddRangeAsync(readings);
            await unitOfWork.CompleteAsync();
            return Result<IReadOnlyCollection<TelemetryReading>>.Success(readings);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<TelemetryReading>>.Failure(TelemetryError.InvalidReadingData);
        }
    }

    public async Task<Result<TelemetryThreshold>> CreateThresholdAsync(CreateThresholdCommand command)
    {
        try
        {
            var threshold = new TelemetryThreshold(command.FarmId, command.DeviceId, command.MetricType,
                command.MinValue, command.MaxValue, command.IsEnabled);
            await thresholdRepository.AddAsync(threshold);
            await unitOfWork.CompleteAsync();
            return Result<TelemetryThreshold>.Success(threshold);
        }
        catch (Exception)
        {
            return Result<TelemetryThreshold>.Failure(TelemetryError.InvalidThresholdData);
        }
    }

    public async Task<Result<TelemetryThreshold>> UpdateThresholdAsync(UpdateThresholdCommand command)
    {
        try
        {
            var threshold = await thresholdRepository.FindByIdAsync(command.ThresholdId);
            if (threshold == null)
                return Result<TelemetryThreshold>.Failure(TelemetryError.ThresholdNotFound);

            threshold.Update(command.MinValue, command.MaxValue, command.IsEnabled);
            await unitOfWork.CompleteAsync();
            return Result<TelemetryThreshold>.Success(threshold);
        }
        catch (Exception)
        {
            return Result<TelemetryThreshold>.Failure(TelemetryError.InvalidThresholdData);
        }
    }

    public async Task<Result> DeleteThresholdAsync(DeleteThresholdCommand command)
    {
        try
        {
            var threshold = await thresholdRepository.FindByIdAsync(command.ThresholdId);
            if (threshold == null)
                return Result.Failure(TelemetryError.ThresholdNotFound);

            thresholdRepository.Remove(threshold);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(TelemetryError.InvalidThresholdData);
        }
    }

    public async Task<Result<TelemetryAlert>> AcknowledgeAlertAsync(AcknowledgeAlertCommand command)
    {
        try
        {
            var alert = await alertRepository.FindByIdAsync(command.AlertId);
            if (alert == null)
                return Result<TelemetryAlert>.Failure(TelemetryError.TelemetryAlertNotFound);

            if (alert.IsAcknowledged)
                return Result<TelemetryAlert>.Failure(TelemetryError.TelemetryAlertAlreadyAcknowledged);

            alert.Acknowledge(command.UserId);
            await unitOfWork.CompleteAsync();
            return Result<TelemetryAlert>.Success(alert);
        }
        catch (Exception)
        {
            return Result<TelemetryAlert>.Failure(TelemetryError.TelemetryAlertNotFound);
        }
    }
}
