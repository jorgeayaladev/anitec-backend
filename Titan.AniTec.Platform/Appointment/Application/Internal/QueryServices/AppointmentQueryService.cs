using Titan.AniTec.Platform.Appointment.Application.CommandServices;
using Titan.AniTec.Platform.Appointment.Domain.Model;
using Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Appointment.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Appointment.Application.Internal.QueryServices;

public class AppointmentQueryService(
    IAppointmentRepository appointmentRepository,
    IAppointmentReminderRepository reminderRepository,
    IAppointmentFollowUpRepository followUpRepository,
    IAppointmentNoteRepository noteRepository,
    IVeterinarianAvailabilityRepository availabilityRepository,
    IAvailabilityBlockRepository blockRepository) : IAppointmentQueryService
{
    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsAsync(
        GetAppointmentsQuery query)
    {
        try
        {
            IReadOnlyCollection<VeterinaryAppointment> appointments;

            if (!string.IsNullOrEmpty(query.Role))
            {
                appointments = query.Role.ToLower() switch
                {
                    "veterinarian" => await appointmentRepository.FindByVeterinarianIdAsync(query.UserId, query.Status),
                    "farmer" => await appointmentRepository.FindByFarmerIdAsync(query.UserId, query.Status),
                    _ => await appointmentRepository.FindByFarmIdAsync(query.UserId, query.Status)
                };
            }
            else
            {
                appointments = await appointmentRepository.FindByFarmIdAsync(query.UserId, query.Status);
            }

            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<VeterinaryAppointment>> GetAppointmentByIdAsync(GetAppointmentByIdQuery query)
    {
        try
        {
            var appointment = await appointmentRepository.FindByIdAsync(query.AppointmentId);
            if (appointment == null)
                return Result<VeterinaryAppointment>.Failure(AppointmentError.AppointmentNotFound);
            return Result<VeterinaryAppointment>.Success(appointment);
        }
        catch (Exception)
        {
            return Result<VeterinaryAppointment>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByStatusAsync(
        GetAppointmentsByStatusQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindByStatusAsync(query.UserId, query.Status);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByTypeAsync(
        GetAppointmentsByTypeQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindByTypeAsync(query.UserId, query.Type);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByRangeAsync(
        GetAppointmentsByRangeQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindByDateRangeAsync(query.UserId, query.Start, query.End);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByDateAsync(
        GetAppointmentsByDateQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindByDateAsync(query.UserId, query.Date);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByWeekAsync(
        GetAppointmentsByWeekQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindByWeekAsync(query.UserId, query.WeekStart);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsUpcomingAsync(
        GetAppointmentsUpcomingQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindUpcomingAsync(query.UserId);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsPastAsync(
        GetAppointmentsPastQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindPastAsync(query.UserId);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByVeterinarianAsync(
        GetAppointmentsByVeterinarianQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindByVeterinarianIdAsync(
                query.VeterinarianId, query.Status);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByFarmerAsync(
        GetAppointmentsByFarmerQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindByFarmerIdAsync(
                query.FarmerId, query.Status);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByFarmAsync(
        GetAppointmentsByFarmQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindByFarmIdAsync(
                query.FarmId, query.Status);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinaryAppointment>>> GetAppointmentsByAnimalAsync(
        GetAppointmentsByAnimalQuery query)
    {
        try
        {
            var appointments = await appointmentRepository.FindByAnimalIdAsync(query.AnimalId);
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Success(appointments);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinaryAppointment>>.Failure(AppointmentError.AppointmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<AppointmentReminder>>> GetRemindersAsync(GetRemindersQuery query)
    {
        try
        {
            var reminders = await reminderRepository.FindByAppointmentIdAsync(query.AppointmentId);
            return Result<IReadOnlyCollection<AppointmentReminder>>.Success(reminders);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<AppointmentReminder>>.Failure(AppointmentError.ReminderNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<AppointmentFollowUp>>> GetFollowUpsAsync(GetFollowUpsQuery query)
    {
        try
        {
            var followUps = await followUpRepository.FindByAppointmentIdAsync(query.AppointmentId);
            return Result<IReadOnlyCollection<AppointmentFollowUp>>.Success(followUps);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<AppointmentFollowUp>>.Failure(AppointmentError.FollowUpNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<AppointmentFollowUp>>> GetPendingFollowUpsAsync(
        GetPendingFollowUpsQuery query)
    {
        try
        {
            var followUps = await followUpRepository.FindPendingAsync(query.FarmId);
            return Result<IReadOnlyCollection<AppointmentFollowUp>>.Success(followUps);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<AppointmentFollowUp>>.Failure(AppointmentError.FollowUpNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<AppointmentNote>>> GetNotesAsync(GetNotesQuery query)
    {
        try
        {
            var notes = await noteRepository.FindByAppointmentIdAsync(query.AppointmentId);
            return Result<IReadOnlyCollection<AppointmentNote>>.Success(notes);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<AppointmentNote>>.Failure(AppointmentError.NoteNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<VeterinarianAvailability>>> GetAvailabilityAsync(
        GetAvailabilityQuery query)
    {
        try
        {
            var availability = await availabilityRepository.FindByVeterinarianIdAsync(query.VeterinarianId);
            return Result<IReadOnlyCollection<VeterinarianAvailability>>.Success(availability);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<VeterinarianAvailability>>.Failure(AppointmentError.AvailabilityNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<AvailabilityBlock>>> GetAvailabilityBlocksAsync(
        GetAvailabilityBlocksQuery query)
    {
        try
        {
            var blocks = await blockRepository.FindByVeterinarianIdAsync(
                query.VeterinarianId, query.StartDate, query.EndDate);
            return Result<IReadOnlyCollection<AvailabilityBlock>>.Success(blocks);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<AvailabilityBlock>>.Failure(AppointmentError.BlockNotFound);
        }
    }
}
