using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;

public class AvailabilityBlock : IAuditableEntity
{
    public AvailabilityBlock(int veterinarianId, DateTime startAt, DateTime endAt, string? reason = null)
    {
        VeterinarianId = veterinarianId;
        StartAt = startAt;
        EndAt = endAt;
        Reason = reason;
    }

    public int Id { get; private set; }
    public int VeterinarianId { get; private set; }
    public DateTime StartAt { get; private set; }
    public DateTime EndAt { get; private set; }
    public string? Reason { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
