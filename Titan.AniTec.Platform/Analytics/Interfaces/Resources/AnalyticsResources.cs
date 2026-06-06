namespace Titan.AniTec.Platform.Analytics.Interfaces.Resources;

public record ScheduledReportResource(int Id, int FarmId, string ReportType, string Format,
    string Schedule, DateTimeOffset? NextRunAt, bool IsActive, DateTimeOffset CreatedAt);
public record ScheduleReportResource(string ReportType, string Format, string Schedule);
