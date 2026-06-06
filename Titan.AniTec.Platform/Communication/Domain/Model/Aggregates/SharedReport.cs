namespace Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;

public class SharedReport
{
    public SharedReport(int reportId, int sharedById, int sharedWithId)
    {
        ReportId = reportId;
        SharedById = sharedById;
        SharedWithId = sharedWithId;
        IsActive = true;
    }

    public int Id { get; private set; }
    public int ReportId { get; private set; }
    public int SharedById { get; private set; }
    public int SharedWithId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset SharedAt { get; private set; }

    public SharedReport RevokeAccess()
    {
        IsActive = false;
        return this;
    }
}
