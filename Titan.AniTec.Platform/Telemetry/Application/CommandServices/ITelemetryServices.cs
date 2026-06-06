using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Telemetry.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Telemetry.Domain.Repositories;

namespace Titan.AniTec.Platform.Telemetry.Application.CommandServices;

public interface ITelemetryCommandService
{
    Task<Result<TelemetryReading>> RecordReadingAsync(RecordReadingCommand command);
    Task<Result<IReadOnlyCollection<TelemetryReading>>> RecordReadingsBatchAsync(RecordReadingsBatchCommand command);
    Task<Result<TelemetryThreshold>> CreateThresholdAsync(CreateThresholdCommand command);
    Task<Result<TelemetryThreshold>> UpdateThresholdAsync(UpdateThresholdCommand command);
    Task<Result> DeleteThresholdAsync(DeleteThresholdCommand command);
    Task<Result<TelemetryAlert>> AcknowledgeAlertAsync(AcknowledgeAlertCommand command);
}

public interface ITelemetryQueryService
{
    Task<Result<IReadOnlyCollection<TelemetryReading>>> GetReadingsAsync(GetReadingsQuery query);
    Task<Result<TelemetryReading>> GetLatestReadingAsync(GetLatestReadingQuery query);
    Task<Result<IReadOnlyCollection<TelemetryReading>>> GetWeightsByAnimalAsync(GetWeightsByAnimalQuery query);
    Task<Result<TelemetryReading>> GetLatestWeightAsync(GetLatestWeightQuery query);
    Task<Result<IReadOnlyCollection<TelemetryReading>>> GetWeightTrendAsync(GetWeightTrendQuery query);
    Task<Result<IReadOnlyCollection<TelemetryReading>>> GetWeightsSummaryAsync(GetWeightsSummaryQuery query);
    Task<Result<IReadOnlyCollection<TelemetryReading>>> GetGpsHistoryAsync(GetGpsHistoryQuery query);
    Task<Result<IReadOnlyCollection<TelemetryThreshold>>> GetThresholdsByDeviceAsync(GetThresholdsByDeviceQuery query);
    Task<Result<IReadOnlyCollection<TelemetryAlert>>> GetTelemetryAlertsAsync(GetTelemetryAlertsQuery query);
    Task<Result<TelemetryAlert>> GetTelemetryAlertByIdAsync(GetTelemetryAlertByIdQuery query);
}
