namespace Titan.AniTec.Platform.Analytics.Domain.Model.Aggregates;

public class ScheduledReport
{
    public ScheduledReport(int farmId, string reportType, string format, string schedule, DateTimeOffset? nextRunAt)
    {
        FarmId = farmId;
        ReportType = reportType;
        Format = format;
        Schedule = schedule;
        NextRunAt = nextRunAt;
        IsActive = true;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string ReportType { get; private set; }
    public string Format { get; private set; }
    public string Schedule { get; private set; }
    public DateTimeOffset? NextRunAt { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public ScheduledReport Cancel()
    {
        IsActive = false;
        return this;
    }
}
