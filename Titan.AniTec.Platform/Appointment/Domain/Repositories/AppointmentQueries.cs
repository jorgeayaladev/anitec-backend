namespace Titan.AniTec.Platform.Appointment.Domain.Repositories;

public record GetAppointmentsQuery(int UserId, string? Status, string? Role,
    DateTime? StartDate, DateTime? EndDate);

public record GetAppointmentByIdQuery(int AppointmentId);

public record GetAppointmentsByDateQuery(int UserId, DateTime Date);

public record GetAppointmentsByWeekQuery(int UserId, DateTime WeekStart);

public record GetAppointmentsByRangeQuery(int UserId, DateTime Start, DateTime End);

public record GetAppointmentsUpcomingQuery(int UserId);

public record GetAppointmentsPastQuery(int UserId);

public record GetAppointmentsByStatusQuery(int UserId, string Status);

public record GetAppointmentsByTypeQuery(int UserId, string Type);

public record GetAppointmentsByVeterinarianQuery(int VeterinarianId, string? Status);

public record GetAppointmentsByFarmerQuery(int FarmerId, string? Status);

public record GetAppointmentsByFarmQuery(int FarmId, string? Status);

public record GetAppointmentsByAnimalQuery(int AnimalId);

public record GetRemindersQuery(int AppointmentId);

public record GetFollowUpsQuery(int AppointmentId);

public record GetPendingFollowUpsQuery(int FarmId);

public record GetNotesQuery(int AppointmentId);

public record GetAvailabilityQuery(int VeterinarianId);

public record GetAvailabilityBlocksQuery(int VeterinarianId, DateTime? StartDate, DateTime? EndDate);
