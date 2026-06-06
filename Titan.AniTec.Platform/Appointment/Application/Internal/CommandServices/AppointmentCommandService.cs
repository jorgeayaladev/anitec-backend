using Titan.AniTec.Platform.Appointment.Application.CommandServices;
using Titan.AniTec.Platform.Appointment.Domain.Model;
using Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Appointment.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Appointment.Application.Internal.CommandServices;

public class AppointmentCommandService(
    IAppointmentRepository appointmentRepository,
    IAppointmentReminderRepository reminderRepository,
    IAppointmentFollowUpRepository followUpRepository,
    IAppointmentNoteRepository noteRepository,
    IVeterinarianAvailabilityRepository availabilityRepository,
    IAvailabilityBlockRepository blockRepository,
    IUnitOfWork unitOfWork) : IAppointmentCommandService
{
    public async Task<Result<VeterinaryAppointment>> CreateAppointmentAsync(CreateAppointmentCommand command)
    {
        try
        {
            var appointment = new VeterinaryAppointment(command.FarmId, command.VeterinarianId, command.FarmerId,
                command.AppointmentType, command.ScheduledAt, command.DurationMinutes,
                command.AnimalId, command.Location, command.Notes);
            await appointmentRepository.AddAsync(appointment);
            await unitOfWork.CompleteAsync();
            return Result<VeterinaryAppointment>.Success(appointment);
        }
        catch (Exception)
        {
            return Result<VeterinaryAppointment>.Failure(AppointmentError.InvalidAppointmentData);
        }
    }

    public async Task<Result<VeterinaryAppointment>> UpdateAppointmentAsync(UpdateAppointmentCommand command)
    {
        try
        {
            var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId);
            if (appointment == null)
                return Result<VeterinaryAppointment>.Failure(AppointmentError.AppointmentNotFound);

            appointment.UpdateDetails(command.AppointmentType, command.ScheduledAt,
                command.DurationMinutes, command.AnimalId, command.Location, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<VeterinaryAppointment>.Success(appointment);
        }
        catch (Exception)
        {
            return Result<VeterinaryAppointment>.Failure(AppointmentError.InvalidAppointmentData);
        }
    }

    public async Task<Result> DeleteAppointmentAsync(DeleteAppointmentCommand command)
    {
        try
        {
            var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId);
            if (appointment == null)
                return Result.Failure(AppointmentError.AppointmentNotFound);

            appointmentRepository.Remove(appointment);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(AppointmentError.InvalidAppointmentData);
        }
    }

    public async Task<Result<VeterinaryAppointment>> ConfirmAppointmentAsync(ConfirmAppointmentCommand command)
    {
        try
        {
            var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId);
            if (appointment == null)
                return Result<VeterinaryAppointment>.Failure(AppointmentError.AppointmentNotFound);

            appointment.Confirm();
            await unitOfWork.CompleteAsync();
            return Result<VeterinaryAppointment>.Success(appointment);
        }
        catch (Exception)
        {
            return Result<VeterinaryAppointment>.Failure(AppointmentError.InvalidAppointmentData);
        }
    }

    public async Task<Result<VeterinaryAppointment>> CompleteAppointmentAsync(CompleteAppointmentCommand command)
    {
        try
        {
            var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId);
            if (appointment == null)
                return Result<VeterinaryAppointment>.Failure(AppointmentError.AppointmentNotFound);

            appointment.Complete();
            await unitOfWork.CompleteAsync();
            return Result<VeterinaryAppointment>.Success(appointment);
        }
        catch (Exception)
        {
            return Result<VeterinaryAppointment>.Failure(AppointmentError.InvalidAppointmentData);
        }
    }

    public async Task<Result<AppointmentReminder>> CreateReminderAsync(CreateReminderCommand command)
    {
        try
        {
            var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId);
            if (appointment == null)
                return Result<AppointmentReminder>.Failure(AppointmentError.AppointmentNotFound);

            var reminder = new AppointmentReminder(command.AppointmentId, command.RemindAtMinutesBefore);
            await reminderRepository.AddAsync(reminder);
            await unitOfWork.CompleteAsync();
            return Result<AppointmentReminder>.Success(reminder);
        }
        catch (Exception)
        {
            return Result<AppointmentReminder>.Failure(AppointmentError.InvalidReminderData);
        }
    }

    public async Task<Result<AppointmentReminder>> UpdateReminderAsync(UpdateReminderCommand command)
    {
        try
        {
            var reminder = await reminderRepository.FindByIdAsync(command.ReminderId);
            if (reminder == null)
                return Result<AppointmentReminder>.Failure(AppointmentError.ReminderNotFound);

            reminder.Update(command.RemindAtMinutesBefore);
            await unitOfWork.CompleteAsync();
            return Result<AppointmentReminder>.Success(reminder);
        }
        catch (Exception)
        {
            return Result<AppointmentReminder>.Failure(AppointmentError.InvalidReminderData);
        }
    }

    public async Task<Result> DeleteReminderAsync(DeleteReminderCommand command)
    {
        try
        {
            var reminder = await reminderRepository.FindByIdAsync(command.ReminderId);
            if (reminder == null)
                return Result.Failure(AppointmentError.ReminderNotFound);

            reminderRepository.Remove(reminder);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(AppointmentError.InvalidReminderData);
        }
    }

    public async Task<Result<AppointmentFollowUp>> CreateFollowUpAsync(CreateFollowUpCommand command)
    {
        try
        {
            var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId);
            if (appointment == null)
                return Result<AppointmentFollowUp>.Failure(AppointmentError.AppointmentNotFound);

            var followUp = new AppointmentFollowUp(command.AppointmentId, command.ScheduledAt, command.Notes);
            await followUpRepository.AddAsync(followUp);
            await unitOfWork.CompleteAsync();
            return Result<AppointmentFollowUp>.Success(followUp);
        }
        catch (Exception)
        {
            return Result<AppointmentFollowUp>.Failure(AppointmentError.InvalidFollowUpData);
        }
    }

    public async Task<Result<AppointmentFollowUp>> UpdateFollowUpAsync(UpdateFollowUpCommand command)
    {
        try
        {
            var followUp = await followUpRepository.FindByIdAsync(command.FollowUpId);
            if (followUp == null)
                return Result<AppointmentFollowUp>.Failure(AppointmentError.FollowUpNotFound);

            followUp.Update(command.ScheduledAt, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<AppointmentFollowUp>.Success(followUp);
        }
        catch (Exception)
        {
            return Result<AppointmentFollowUp>.Failure(AppointmentError.InvalidFollowUpData);
        }
    }

    public async Task<Result<AppointmentFollowUp>> CompleteFollowUpAsync(CompleteFollowUpCommand command)
    {
        try
        {
            var followUp = await followUpRepository.FindByIdAsync(command.FollowUpId);
            if (followUp == null)
                return Result<AppointmentFollowUp>.Failure(AppointmentError.FollowUpNotFound);

            followUp.Complete();
            await unitOfWork.CompleteAsync();
            return Result<AppointmentFollowUp>.Success(followUp);
        }
        catch (Exception)
        {
            return Result<AppointmentFollowUp>.Failure(AppointmentError.InvalidFollowUpData);
        }
    }

    public async Task<Result> DeleteFollowUpAsync(DeleteFollowUpCommand command)
    {
        try
        {
            var followUp = await followUpRepository.FindByIdAsync(command.FollowUpId);
            if (followUp == null)
                return Result.Failure(AppointmentError.FollowUpNotFound);

            followUpRepository.Remove(followUp);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(AppointmentError.InvalidFollowUpData);
        }
    }

    public async Task<Result<AppointmentNote>> CreateNoteAsync(CreateNoteCommand command)
    {
        try
        {
            var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId);
            if (appointment == null)
                return Result<AppointmentNote>.Failure(AppointmentError.AppointmentNotFound);

            var note = new AppointmentNote(command.AppointmentId, command.Content);
            await noteRepository.AddAsync(note);
            await unitOfWork.CompleteAsync();
            return Result<AppointmentNote>.Success(note);
        }
        catch (Exception)
        {
            return Result<AppointmentNote>.Failure(AppointmentError.InvalidNoteData);
        }
    }

    public async Task<Result<AppointmentNote>> UpdateNoteAsync(UpdateNoteCommand command)
    {
        try
        {
            var note = await noteRepository.FindByIdAsync(command.NoteId);
            if (note == null)
                return Result<AppointmentNote>.Failure(AppointmentError.NoteNotFound);

            note.Update(command.Content);
            await unitOfWork.CompleteAsync();
            return Result<AppointmentNote>.Success(note);
        }
        catch (Exception)
        {
            return Result<AppointmentNote>.Failure(AppointmentError.InvalidNoteData);
        }
    }

    public async Task<Result> DeleteNoteAsync(DeleteNoteCommand command)
    {
        try
        {
            var note = await noteRepository.FindByIdAsync(command.NoteId);
            if (note == null)
                return Result.Failure(AppointmentError.NoteNotFound);

            noteRepository.Remove(note);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(AppointmentError.InvalidNoteData);
        }
    }

    public async Task<Result> UpdateAvailabilityAsync(UpdateAvailabilityCommand command)
    {
        try
        {
            var existing = await availabilityRepository.FindByVeterinarianIdAsync(command.VeterinarianId);
            foreach (var slot in existing)
                availabilityRepository.Remove(slot);

            foreach (var slot in command.Slots)
            {
                var availability = new VeterinarianAvailability(command.VeterinarianId,
                    slot.DayOfWeek, slot.StartTime, slot.EndTime, slot.IsAvailable);
                await availabilityRepository.AddAsync(availability);
            }

            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(AppointmentError.InvalidAvailabilityData);
        }
    }

    public async Task<Result> BlockAvailabilityAsync(BlockAvailabilityCommand command)
    {
        try
        {
            var block = new AvailabilityBlock(command.VeterinarianId,
                command.StartAt, command.EndAt, command.Reason);
            await blockRepository.AddAsync(block);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(AppointmentError.InvalidBlockData);
        }
    }

    public async Task<Result> UnblockAvailabilityAsync(UnblockAvailabilityCommand command)
    {
        try
        {
            var block = await blockRepository.FindByIdAsync(command.BlockId);
            if (block == null)
                return Result.Failure(AppointmentError.BlockNotFound);

            blockRepository.Remove(block);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(AppointmentError.InvalidBlockData);
        }
    }
}
