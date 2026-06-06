using Titan.AniTec.Platform.Appointment.Domain.Model;
using Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Appointment.Domain.Repositories;
using Titan.AniTec.Platform.Appointment.Interfaces.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Appointment.Interfaces.Assemblers;

public static class AppointmentAssembler
{
    public static AppointmentResource ToResource(VeterinaryAppointment appointment)
        => new(appointment.Id, appointment.FarmId, appointment.VeterinarianId, appointment.FarmerId,
            appointment.AnimalId, appointment.AppointmentType, appointment.Status,
            appointment.ScheduledAt, appointment.DurationMinutes, appointment.Location, appointment.Notes);

    public static CreateAppointmentCommand ToCommand(int farmId, CreateAppointmentResource resource)
        => new(farmId, resource.VeterinarianId, resource.FarmerId, resource.AppointmentType,
            resource.ScheduledAt, resource.DurationMinutes, resource.AnimalId,
            resource.Location, resource.Notes);

    public static UpdateAppointmentCommand ToCommand(int appointmentId, UpdateAppointmentResource resource)
        => new(appointmentId, resource.AppointmentType, resource.ScheduledAt,
            resource.DurationMinutes, resource.AnimalId, resource.Location, resource.Notes);

    public static ReminderResource ToResource(AppointmentReminder reminder)
        => new(reminder.Id, reminder.AppointmentId, reminder.RemindAtMinutesBefore, reminder.SentAt);

    public static CreateReminderCommand ToCommand(int appointmentId, CreateReminderResource resource)
        => new(appointmentId, resource.RemindAtMinutesBefore);

    public static UpdateReminderCommand ToCommand(int reminderId, UpdateReminderResource resource)
        => new(reminderId, resource.RemindAtMinutesBefore);

    public static FollowUpResource ToResource(AppointmentFollowUp followUp)
        => new(followUp.Id, followUp.AppointmentId, followUp.ScheduledAt,
            followUp.CompletedAt, followUp.Status, followUp.Notes);

    public static CreateFollowUpCommand ToCommand(int appointmentId, CreateFollowUpResource resource)
        => new(appointmentId, resource.ScheduledAt, resource.Notes);

    public static UpdateFollowUpCommand ToCommand(int followUpId, UpdateFollowUpResource resource)
        => new(followUpId, resource.ScheduledAt, resource.Notes);

    public static NoteResource ToResource(AppointmentNote note)
        => new(note.Id, note.AppointmentId, note.Content);

    public static CreateNoteCommand ToCommand(int appointmentId, CreateNoteResource resource)
        => new(appointmentId, resource.Content);

    public static UpdateNoteCommand ToCommand(int noteId, UpdateNoteResource resource)
        => new(noteId, resource.Content);

    public static AvailabilitySlotResource ToResource(VeterinarianAvailability availability)
        => new(availability.DayOfWeek, availability.StartTime, availability.EndTime, availability.IsAvailable);

    public static AvailabilityBlockResource ToResource(AvailabilityBlock block)
        => new(block.Id, block.VeterinarianId, block.StartAt, block.EndAt, block.Reason);
}

public static class AppointmentActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsFailure)
        {
            return result.Error switch
            {
                AppointmentError.AppointmentNotFound or
                AppointmentError.ReminderNotFound or
                AppointmentError.FollowUpNotFound or
                AppointmentError.NoteNotFound or
                AppointmentError.AvailabilityNotFound or
                AppointmentError.BlockNotFound => new NotFoundObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                AppointmentError.FollowUpAlreadyCompleted => new ConflictObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                _ => new BadRequestObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() })
            };
        }
        return new OkObjectResult(result.Value);
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsFailure)
            return ToActionResult(result);
        return new CreatedResult(string.Empty, result.Value);
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsFailure)
        {
            return result.Error switch
            {
                AppointmentError.AppointmentNotFound or
                AppointmentError.ReminderNotFound or
                AppointmentError.FollowUpNotFound or
                AppointmentError.NoteNotFound or
                AppointmentError.BlockNotFound => new NotFoundObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                _ => new BadRequestObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() })
            };
        }
        return new OkResult();
    }
}
