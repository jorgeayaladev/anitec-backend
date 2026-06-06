namespace Titan.AniTec.Platform.Analytics.Domain.Repositories;

public record ScheduleReportCommand(int UserId, string ReportType, string Format, string Schedule);
public record CancelScheduledReportCommand(int UserId, int ScheduleId);
