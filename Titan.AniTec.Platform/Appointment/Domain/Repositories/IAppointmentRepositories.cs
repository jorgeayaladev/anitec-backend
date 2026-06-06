using Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Appointment.Domain.Repositories;

public interface IAppointmentRepository : IBaseRepository<VeterinaryAppointment>
{
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindByFarmIdAsync(int farmId, string? status = null,
        DateTime? startDate = null, DateTime? endDate = null);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindByVeterinarianIdAsync(int veterinarianId,
        string? status = null, DateTime? startDate = null, DateTime? endDate = null);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindByFarmerIdAsync(int farmerId,
        string? status = null, DateTime? startDate = null, DateTime? endDate = null);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindByAnimalIdAsync(int animalId);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindByDateRangeAsync(int userId, DateTime start, DateTime end);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindByDateAsync(int userId, DateTime date);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindByWeekAsync(int userId, DateTime weekStart);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindUpcomingAsync(int userId);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindPastAsync(int userId);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindByStatusAsync(int userId, string status);
    Task<IReadOnlyCollection<VeterinaryAppointment>> FindByTypeAsync(int userId, string type);
}

public interface IAppointmentReminderRepository : IBaseRepository<AppointmentReminder>
{
    Task<IReadOnlyCollection<AppointmentReminder>> FindByAppointmentIdAsync(int appointmentId);
}

public interface IAppointmentFollowUpRepository : IBaseRepository<AppointmentFollowUp>
{
    Task<IReadOnlyCollection<AppointmentFollowUp>> FindByAppointmentIdAsync(int appointmentId);
    Task<IReadOnlyCollection<AppointmentFollowUp>> FindPendingAsync(int farmId);
}

public interface IAppointmentNoteRepository : IBaseRepository<AppointmentNote>
{
    Task<IReadOnlyCollection<AppointmentNote>> FindByAppointmentIdAsync(int appointmentId);
}

public interface IVeterinarianAvailabilityRepository : IBaseRepository<VeterinarianAvailability>
{
    Task<IReadOnlyCollection<VeterinarianAvailability>> FindByVeterinarianIdAsync(int veterinarianId);
    Task<VeterinarianAvailability?> FindByVeterinarianAndDayAsync(int veterinarianId, int dayOfWeek);
}

public interface IAvailabilityBlockRepository : IBaseRepository<AvailabilityBlock>
{
    Task<IReadOnlyCollection<AvailabilityBlock>> FindByVeterinarianIdAsync(int veterinarianId,
        DateTime? startDate = null, DateTime? endDate = null);
    Task<IReadOnlyCollection<AvailabilityBlock>> FindOverlappingAsync(int veterinarianId,
        DateTime startAt, DateTime endAt);
}
