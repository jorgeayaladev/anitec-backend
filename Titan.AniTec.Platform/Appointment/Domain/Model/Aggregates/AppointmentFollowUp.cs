using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;

public class AppointmentFollowUp : IAuditableEntity
{
    public AppointmentFollowUp(int appointmentId, DateTime scheduledAt, string? notes = null)
    {
        AppointmentId = appointmentId;
        ScheduledAt = scheduledAt;
        Status = "pending";
        Notes = notes;
    }

    public int Id { get; private set; }
    public int AppointmentId { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string Status { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public AppointmentFollowUp Update(DateTime scheduledAt, string? notes)
    {
        ScheduledAt = scheduledAt;
        Notes = notes;
        return this;
    }

    public AppointmentFollowUp Complete()
    {
        Status = "completed";
        CompletedAt = DateTime.UtcNow;
        return this;
    }
}
