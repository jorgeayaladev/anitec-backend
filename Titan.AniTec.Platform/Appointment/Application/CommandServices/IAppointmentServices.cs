using Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Appointment.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Appointment.Application.CommandServices;

public interface IAppointmentCommandService
{
    Task<Result<VeterinaryAppointment>> CreateAppointmentAsync(CreateAppointmentCommand command);
    Task<Result<VeterinaryAppointment>> UpdateAppointmentAsync(UpdateAppointmentCommand command);
    Task<Result> DeleteAppointmentAsync(DeleteAppointmentCommand command);
    Task<Result<VeterinaryAppointment>> ConfirmAppointmentAsync(ConfirmAppointmentCommand command);
    Task<Result<VeterinaryAppointment>> CompleteAppointmentAsync(CompleteAppointmentCommand command);
    Task<Result<AppointmentReminder>> CreateReminderAsync(CreateReminderCommand command);
    Task<Result<AppointmentReminder>> UpdateReminderAsync(UpdateReminderCommand command);
    Task<Result> DeleteReminderAsync(DeleteReminderCommand command);
    Task<Result<AppointmentFollowUp>> CreateFollowUpAsync(CreateFollowUpCommand command);
    Task<Result<AppointmentFollowUp>> UpdateFollowUpAsync(UpdateFollowUpCommand command);
    Task<Result<AppointmentFollowUp>> CompleteFollowUpAsync(CompleteFollowUpCommand command);
    Task<Result> DeleteFollowUpAsync(DeleteFollowUpCommand command);
    Task<Result<AppointmentNote>> CreateNoteAsync(CreateNoteCommand command);
    Task<Result<AppointmentNote>> UpdateNoteAsync(UpdateNoteCommand command);
    Task<Result> DeleteNoteAsync(DeleteNoteCommand command);
    Task<Result> UpdateAvailabilityAsync(UpdateAvailabilityCommand command);
    Task<Result> BlockAvailabilityAsync(BlockAvailabilityCommand command);
    Task<Result> UnblockAvailabilityAsync(UnblockAvailabilityCommand command);
}

public interface IAppointmentQueryService
{
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsAsync(GetAppointmentsQuery query);
    Task<Result<VeterinaryAppointment>> GetAppointmentByIdAsync(GetAppointmentByIdQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByStatusAsync(GetAppointmentsByStatusQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByTypeAsync(GetAppointmentsByTypeQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByRangeAsync(GetAppointmentsByRangeQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByDateAsync(GetAppointmentsByDateQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByWeekAsync(GetAppointmentsByWeekQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsUpcomingAsync(GetAppointmentsUpcomingQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsPastAsync(GetAppointmentsPastQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByVeterinarianAsync(GetAppointmentsByVeterinarianQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByFarmerAsync(GetAppointmentsByFarmerQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByFarmAsync(GetAppointmentsByFarmQuery query);
    Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByAnimalAsync(GetAppointmentsByAnimalQuery query);
    Task<Result<IReadOnlyCollection<AppointmentReminder>>> GetRemindersAsync(GetRemindersQuery query);
    Task<Result<IReadOnlyCollection<AppointmentFollowUp>>> GetFollowUpsAsync(GetFollowUpsQuery query);
    Task<Result<IReadOnlyCollection<AppointmentFollowUp>>> GetPendingFollowUpsAsync(GetPendingFollowUpsQuery query);
    Task<Result<IReadOnlyCollection<AppointmentNote>>> GetNotesAsync(GetNotesQuery query);
    Task<Result<IReadOnlyCollection<VeterinarianAvailability>>> GetAvailabilityAsync(GetAvailabilityQuery query);
    Task<Result<IReadOnlyCollection<AvailabilityBlock>>> GetAvailabilityBlocksAsync(GetAvailabilityBlocksQuery query);
}
