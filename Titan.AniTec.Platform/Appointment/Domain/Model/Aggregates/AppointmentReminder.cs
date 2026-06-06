using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;

public class AppointmentReminder : IAuditableEntity
{
    public AppointmentReminder(int appointmentId, int remindAtMinutesBefore)
    {
        AppointmentId = appointmentId;
        RemindAtMinutesBefore = remindAtMinutesBefore;
    }

    public int Id { get; private set; }
    public int AppointmentId { get; private set; }
    public int RemindAtMinutesBefore { get; private set; }
    public DateTime? SentAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public AppointmentReminder Update(int remindAtMinutesBefore)
    {
        RemindAtMinutesBefore = remindAtMinutesBefore;
        return this;
    }

    public AppointmentReminder MarkSent()
    {
        SentAt = DateTime.UtcNow;
        return this;
    }
}
