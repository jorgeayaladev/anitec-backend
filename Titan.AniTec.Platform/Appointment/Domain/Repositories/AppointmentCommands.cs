namespace Titan.AniTec.Platform.Appointment.Domain.Repositories;

public record CreateAppointmentCommand(int FarmId, int VeterinarianId, int FarmerId, string AppointmentType,
    DateTime ScheduledAt, int DurationMinutes, int? AnimalId, string? Location, string? Notes);

public record UpdateAppointmentCommand(int AppointmentId, string AppointmentType,
    DateTime ScheduledAt, int DurationMinutes, int? AnimalId, string? Location, string? Notes);

public record DeleteAppointmentCommand(int AppointmentId);

public record ConfirmAppointmentCommand(int AppointmentId);

public record CompleteAppointmentCommand(int AppointmentId);

public record CreateReminderCommand(int AppointmentId, int RemindAtMinutesBefore);

public record UpdateReminderCommand(int ReminderId, int RemindAtMinutesBefore);

public record DeleteReminderCommand(int ReminderId);

public record CreateFollowUpCommand(int AppointmentId, DateTime ScheduledAt, string? Notes);

public record UpdateFollowUpCommand(int FollowUpId, DateTime ScheduledAt, string? Notes);

public record CompleteFollowUpCommand(int FollowUpId);

public record DeleteFollowUpCommand(int FollowUpId);

public record CreateNoteCommand(int AppointmentId, string Content);

public record UpdateNoteCommand(int NoteId, string Content);

public record DeleteNoteCommand(int NoteId);

public record UpdateAvailabilityCommand(int VeterinarianId, IReadOnlyCollection<AvailabilitySlot> Slots);

public record AvailabilitySlot(int DayOfWeek, TimeSpan StartTime, TimeSpan EndTime, bool IsAvailable);

public record BlockAvailabilityCommand(int VeterinarianId, DateTime StartAt, DateTime EndAt, string? Reason);

public record UnblockAvailabilityCommand(int BlockId);
