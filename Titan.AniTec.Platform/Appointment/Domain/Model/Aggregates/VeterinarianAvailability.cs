using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;

public class VeterinarianAvailability : IAuditableEntity
{
    public VeterinarianAvailability(int veterinarianId, int dayOfWeek,
        TimeSpan startTime, TimeSpan endTime, bool isAvailable = true)
    {
        VeterinarianId = veterinarianId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        IsAvailable = isAvailable;
    }

    public int Id { get; private set; }
    public int VeterinarianId { get; private set; }
    public int DayOfWeek { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public VeterinarianAvailability Update(int dayOfWeek, TimeSpan startTime, TimeSpan endTime, bool isAvailable)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        IsAvailable = isAvailable;
        return this;
    }
}
