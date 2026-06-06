using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;

public class AppointmentNote : IAuditableEntity
{
    public AppointmentNote(int appointmentId, string content)
    {
        AppointmentId = appointmentId;
        Content = content;
    }

    public int Id { get; private set; }
    public int AppointmentId { get; private set; }
    public string Content { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public AppointmentNote Update(string content)
    {
        Content = content;
        return this;
    }
}
