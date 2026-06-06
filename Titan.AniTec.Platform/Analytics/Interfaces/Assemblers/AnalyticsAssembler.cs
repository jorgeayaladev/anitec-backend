using Titan.AniTec.Platform.Analytics.Domain.Model;
using Titan.AniTec.Platform.Analytics.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Analytics.Domain.Repositories;
using Titan.AniTec.Platform.Analytics.Interfaces.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Analytics.Interfaces.Assemblers;

public static class AnalyticsAssembler
{
    public static ScheduledReportResource ToResource(this ScheduledReport entity) =>
        new(entity.Id, entity.FarmId, entity.ReportType, entity.Format,
            entity.Schedule, entity.NextRunAt, entity.IsActive, entity.CreatedAt);

    public static ScheduleReportCommand ToCommand(int farmId, ScheduleReportResource resource) =>
        new(farmId, resource.ReportType, resource.Format, resource.Schedule);
}

public static class AnalyticsActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess) return new OkObjectResult(result.Data);
        return MapError(result.Error);
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess) return new OkResult();
        return MapError(result.Error);
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess) return new CreatedResult(string.Empty, result.Data);
        return MapError(result.Error);
    }

    private static IActionResult MapError(Enum? error) => error switch
    {
        AnalyticsError.ScheduledReportNotFound or
        AnalyticsError.DashboardDataNotFound
            => new NotFoundObjectResult(new { Error = error!.ToString() }),

        AnalyticsError.InvalidDateRange or
        AnalyticsError.InvalidReportType or
        AnalyticsError.InvalidScheduleData or
        AnalyticsError.InvalidExportFormat
            => new BadRequestObjectResult(new { Error = error!.ToString() }),

        _ => new StatusCodeResult(500)
    };
}
