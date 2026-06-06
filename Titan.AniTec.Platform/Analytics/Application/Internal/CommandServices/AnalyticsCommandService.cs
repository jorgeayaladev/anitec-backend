using Titan.AniTec.Platform.Analytics.Domain.Model;
using Titan.AniTec.Platform.Analytics.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Analytics.Domain.Repositories;
using Titan.AniTec.Platform.Analytics.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Analytics.Application.Internal.CommandServices;

public class AnalyticsCommandService(
    IScheduledReportRepository scheduledReportRepository,
    IUnitOfWork unitOfWork) : IAnalyticsCommandService
{
    public async Task<Result<ScheduledReport>> ScheduleReportAsync(ScheduleReportCommand command)
    {
        try
        {
            var report = new ScheduledReport(command.UserId, command.ReportType, command.Format, command.Schedule, null);
            await scheduledReportRepository.AddAsync(report);
            await unitOfWork.CompleteAsync();
            return Result<ScheduledReport>.Success(report);
        }
        catch { return Result<ScheduledReport>.Failure(AnalyticsError.InvalidScheduleData); }
    }

    public async Task<Result> CancelScheduledReportAsync(CancelScheduledReportCommand command)
    {
        try
        {
            var report = await scheduledReportRepository.FindByIdAsync(command.ScheduleId);
            if (report == null) return Result.Failure(AnalyticsError.ScheduledReportNotFound);
            report.Cancel();
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(AnalyticsError.ScheduledReportNotFound); }
    }
}
