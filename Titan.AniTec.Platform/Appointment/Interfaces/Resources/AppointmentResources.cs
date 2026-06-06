namespace Titan.AniTec.Platform.Appointment.Interfaces.Resources;

public record AppointmentResource(
    int Id, int FarmId, int VeterinarianId, int FarmerId, int? AnimalId,
    string AppointmentType, string Status, DateTime ScheduledAt,
    int DurationMinutes, string? Location, string? Notes);

public record CreateAppointmentResource(
    int VeterinarianId, int FarmerId, string AppointmentType, DateTime ScheduledAt,
    int DurationMinutes, int? AnimalId, string? Location, string? Notes);

public record UpdateAppointmentResource(
    string AppointmentType, DateTime ScheduledAt, int DurationMinutes,
    int? AnimalId, string? Location, string? Notes);

public record ReminderResource(int Id, int AppointmentId, int RemindAtMinutesBefore, DateTime? SentAt);

public record CreateReminderResource(int RemindAtMinutesBefore);

public record UpdateReminderResource(int RemindAtMinutesBefore);

public record FollowUpResource(int Id, int AppointmentId, DateTime ScheduledAt,
    DateTime? CompletedAt, string Status, string? Notes);

public record CreateFollowUpResource(DateTime ScheduledAt, string? Notes);

public record UpdateFollowUpResource(DateTime ScheduledAt, string? Notes);

public record NoteResource(int Id, int AppointmentId, string Content);

public record CreateNoteResource(string Content);

public record UpdateNoteResource(string Content);

public record AvailabilitySlotResource(int DayOfWeek, TimeSpan StartTime, TimeSpan EndTime, bool IsAvailable);

public record UpdateAvailabilityResource(IReadOnlyCollection<AvailabilitySlotResource> Slots);

public record BlockAvailabilityResource(DateTime StartAt, DateTime EndAt, string? Reason);

public record AvailabilityBlockResource(int Id, int VeterinarianId, DateTime StartAt, DateTime EndAt, string? Reason);
