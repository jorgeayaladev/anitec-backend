using Titan.AniTec.Platform.Appointment.Application.CommandServices;
using Titan.AniTec.Platform.Appointment.Domain.Repositories;
using Titan.AniTec.Platform.Appointment.Interfaces.Assemblers;
using Titan.AniTec.Platform.Appointment.Interfaces.Resources;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Appointment.Interfaces.Controllers;

[ApiController]
[Route("api/appointments")]
[Authorize]
public class AppointmentController(
    IAppointmentQueryService queryService,
    IAppointmentCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    // --- Citas ---
    [HttpGet]
    public async Task<IActionResult> GetAllAppointments([FromQuery] string? status, [FromQuery] string? role)
    {
        var query = new GetAppointmentsQuery(CurrentUserId, status, role, null, null);
        var result = await queryService.GetAppointmentsAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("{appointmentId:int}")]
    public async Task<IActionResult> GetAppointmentById(int appointmentId)
    {
        var query = new GetAppointmentByIdQuery(appointmentId);
        var result = await queryService.GetAppointmentByIdAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentResource resource)
    {
        var command = AppointmentAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.CreateAppointmentAsync(command);
        return AppointmentActionResultAssembler.ToCreatedActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpPut("{appointmentId:int}")]
    public async Task<IActionResult> UpdateAppointment(int appointmentId, [FromBody] UpdateAppointmentResource resource)
    {
        var command = AppointmentAssembler.ToCommand(appointmentId, resource);
        var result = await commandService.UpdateAppointmentAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpDelete("{appointmentId:int}")]
    public async Task<IActionResult> DeleteAppointment(int appointmentId)
    {
        var command = new DeleteAppointmentCommand(appointmentId);
        var result = await commandService.DeleteAppointmentAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(result);
    }

    // --- Filtros de fecha ---
    [HttpGet("today")]
    public async Task<IActionResult> GetTodayAppointments()
    {
        var query = new GetAppointmentsByDateQuery(CurrentUserId, DateTime.UtcNow);
        var result = await queryService.GetAppointmentsByDateAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("tomorrow")]
    public async Task<IActionResult> GetTomorrowAppointments()
    {
        var query = new GetAppointmentsByDateQuery(CurrentUserId, DateTime.UtcNow.AddDays(1));
        var result = await queryService.GetAppointmentsByDateAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("this-week")]
    public async Task<IActionResult> GetThisWeekAppointments()
    {
        var weekStart = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
        var query = new GetAppointmentsByWeekQuery(CurrentUserId, weekStart);
        var result = await queryService.GetAppointmentsByWeekAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("next-week")]
    public async Task<IActionResult> GetNextWeekAppointments()
    {
        var weekStart = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek + 7);
        var query = new GetAppointmentsByWeekQuery(CurrentUserId, weekStart);
        var result = await queryService.GetAppointmentsByWeekAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("by-date-range")]
    public async Task<IActionResult> GetAppointmentsByDateRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetAppointmentsByRangeQuery(CurrentUserId, start, end);
        var result = await queryService.GetAppointmentsByRangeAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingAppointments()
    {
        var query = new GetAppointmentsUpcomingQuery(CurrentUserId);
        var result = await queryService.GetAppointmentsUpcomingAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("past")]
    public async Task<IActionResult> GetPastAppointments()
    {
        var query = new GetAppointmentsPastQuery(CurrentUserId);
        var result = await queryService.GetAppointmentsPastAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingAppointments()
    {
        var query = new GetAppointmentsByStatusQuery(CurrentUserId, "pending");
        var result = await queryService.GetAppointmentsByStatusAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("confirmed")]
    public async Task<IActionResult> GetConfirmedAppointments()
    {
        var query = new GetAppointmentsByStatusQuery(CurrentUserId, "confirmed");
        var result = await queryService.GetAppointmentsByStatusAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("canceled")]
    public async Task<IActionResult> GetCanceledAppointments()
    {
        var query = new GetAppointmentsByStatusQuery(CurrentUserId, "canceled");
        var result = await queryService.GetAppointmentsByStatusAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetCompletedAppointments()
    {
        var query = new GetAppointmentsByStatusQuery(CurrentUserId, "completed");
        var result = await queryService.GetAppointmentsByStatusAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    // --- Por entidad ---
    [HttpGet("by-veterinarian/{veterinarianId:int}")]
    public async Task<IActionResult> GetAppointmentsByVeterinarian(int veterinarianId, [FromQuery] string? status)
    {
        var query = new GetAppointmentsByVeterinarianQuery(veterinarianId, status);
        var result = await queryService.GetAppointmentsByVeterinarianAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("by-farmer/{farmerId:int}")]
    public async Task<IActionResult> GetAppointmentsByFarmer(int farmerId, [FromQuery] string? status)
    {
        var query = new GetAppointmentsByFarmerQuery(farmerId, status);
        var result = await queryService.GetAppointmentsByFarmerAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("by-farm/{farmId:int}")]
    public async Task<IActionResult> GetAppointmentsByFarm(int farmId, [FromQuery] string? status)
    {
        var query = new GetAppointmentsByFarmQuery(farmId, status);
        var result = await queryService.GetAppointmentsByFarmAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("by-animal/{animalId:int}")]
    public async Task<IActionResult> GetAppointmentsByAnimal(int animalId)
    {
        var query = new GetAppointmentsByAnimalQuery(animalId);
        var result = await queryService.GetAppointmentsByAnimalAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("by-type/{type}")]
    public async Task<IActionResult> GetAppointmentsByType(string type)
    {
        var query = new GetAppointmentsByTypeQuery(CurrentUserId, type);
        var result = await queryService.GetAppointmentsByTypeAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    // --- Acciones de estado ---
    [HttpPut("{appointmentId:int}/confirm")]
    public async Task<IActionResult> ConfirmAppointment(int appointmentId)
    {
        var command = new ConfirmAppointmentCommand(appointmentId);
        var result = await commandService.ConfirmAppointmentAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpPut("{appointmentId:int}/complete")]
    public async Task<IActionResult> CompleteAppointment(int appointmentId)
    {
        var command = new CompleteAppointmentCommand(appointmentId);
        var result = await commandService.CompleteAppointmentAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    // --- Recordatorios ---
    [HttpGet("{appointmentId:int}/reminders")]
    public async Task<IActionResult> GetReminders(int appointmentId)
    {
        var query = new GetRemindersQuery(appointmentId);
        var result = await queryService.GetRemindersAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpPost("{appointmentId:int}/reminders")]
    public async Task<IActionResult> CreateReminder(int appointmentId, [FromBody] CreateReminderResource resource)
    {
        var command = AppointmentAssembler.ToCommand(appointmentId, resource);
        var result = await commandService.CreateReminderAsync(command);
        return AppointmentActionResultAssembler.ToCreatedActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpPut("reminders/{reminderId:int}")]
    public async Task<IActionResult> UpdateReminder(int reminderId, [FromBody] UpdateReminderResource resource)
    {
        var command = AppointmentAssembler.ToCommand(reminderId, resource);
        var result = await commandService.UpdateReminderAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpDelete("reminders/{reminderId:int}")]
    public async Task<IActionResult> DeleteReminder(int reminderId)
    {
        var command = new DeleteReminderCommand(reminderId);
        var result = await commandService.DeleteReminderAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(result);
    }

    // --- Seguimientos ---
    [HttpGet("{appointmentId:int}/follow-ups")]
    public async Task<IActionResult> GetFollowUps(int appointmentId)
    {
        var query = new GetFollowUpsQuery(appointmentId);
        var result = await queryService.GetFollowUpsAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpPost("{appointmentId:int}/follow-ups")]
    public async Task<IActionResult> CreateFollowUp(int appointmentId, [FromBody] CreateFollowUpResource resource)
    {
        var command = AppointmentAssembler.ToCommand(appointmentId, resource);
        var result = await commandService.CreateFollowUpAsync(command);
        return AppointmentActionResultAssembler.ToCreatedActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpPut("follow-ups/{followUpId:int}")]
    public async Task<IActionResult> UpdateFollowUp(int followUpId, [FromBody] UpdateFollowUpResource resource)
    {
        var command = AppointmentAssembler.ToCommand(followUpId, resource);
        var result = await commandService.UpdateFollowUpAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpPut("follow-ups/{followUpId:int}/complete")]
    public async Task<IActionResult> CompleteFollowUp(int followUpId)
    {
        var command = new CompleteFollowUpCommand(followUpId);
        var result = await commandService.CompleteFollowUpAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpGet("follow-ups/pending")]
    public async Task<IActionResult> GetPendingFollowUps()
    {
        var query = new GetPendingFollowUpsQuery(CurrentUserId);
        var result = await queryService.GetPendingFollowUpsAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    // --- Notas ---
    [HttpGet("{appointmentId:int}/notes")]
    public async Task<IActionResult> GetNotes(int appointmentId)
    {
        var query = new GetNotesQuery(appointmentId);
        var result = await queryService.GetNotesAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpPost("{appointmentId:int}/notes")]
    public async Task<IActionResult> CreateNote(int appointmentId, [FromBody] CreateNoteResource resource)
    {
        var command = AppointmentAssembler.ToCommand(appointmentId, resource);
        var result = await commandService.CreateNoteAsync(command);
        return AppointmentActionResultAssembler.ToCreatedActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpPut("{appointmentId:int}/notes/{noteId:int}")]
    public async Task<IActionResult> UpdateNote(int appointmentId, int noteId, [FromBody] UpdateNoteResource resource)
    {
        var command = AppointmentAssembler.ToCommand(noteId, resource);
        var result = await commandService.UpdateNoteAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    [HttpDelete("{appointmentId:int}/notes/{noteId:int}")]
    public async Task<IActionResult> DeleteNote(int appointmentId, int noteId)
    {
        var command = new DeleteNoteCommand(noteId);
        var result = await commandService.DeleteNoteAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("{appointmentId:int}/summary")]
    public async Task<IActionResult> GetAppointmentSummary(int appointmentId)
    {
        var query = new GetAppointmentByIdQuery(appointmentId);
        var result = await queryService.GetAppointmentByIdAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(AppointmentAssembler.ToResource));
    }

    // --- Disponibilidad ---
    [HttpGet("availability")]
    public async Task<IActionResult> GetAvailability()
    {
        var query = new GetAvailabilityQuery(CurrentUserId);
        var result = await queryService.GetAvailabilityAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("availability/date/{date}")]
    public async Task<IActionResult> GetAvailabilityByDate(DateTime date)
    {
        var query = new GetAvailabilityQuery(CurrentUserId);
        var result = await queryService.GetAvailabilityAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("availability/range")]
    public async Task<IActionResult> GetAvailabilityRange([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var query = new GetAvailabilityBlocksQuery(CurrentUserId, start, end);
        var result = await queryService.GetAvailabilityBlocksAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("slots")]
    public async Task<IActionResult> GetSlots()
    {
        var query = new GetAvailabilityQuery(CurrentUserId);
        var result = await queryService.GetAvailabilityAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpGet("slots/date/{date}")]
    public async Task<IActionResult> GetSlotsByDate(DateTime date)
    {
        var query = new GetAvailabilityQuery(CurrentUserId);
        var result = await queryService.GetAvailabilityAsync(query);
        return AppointmentActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(AppointmentAssembler.ToResource).ToList()));
    }

    [HttpPut("availability")]
    public async Task<IActionResult> UpdateAvailability([FromBody] UpdateAvailabilityResource resource)
    {
        var slots = resource.Slots.Select(s => new AvailabilitySlot(
            s.DayOfWeek, s.StartTime, s.EndTime, s.IsAvailable)).ToList();
        var command = new UpdateAvailabilityCommand(CurrentUserId, slots);
        var result = await commandService.UpdateAvailabilityAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(result);
    }

    [HttpPost("availability/block")]
    public async Task<IActionResult> BlockAvailability([FromBody] BlockAvailabilityResource resource)
    {
        var command = new BlockAvailabilityCommand(CurrentUserId, resource.StartAt, resource.EndAt, resource.Reason);
        var result = await commandService.BlockAvailabilityAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(result);
    }

    [HttpDelete("availability/block/{blockId:int}")]
    public async Task<IActionResult> UnblockAvailability(int blockId)
    {
        var command = new UnblockAvailabilityCommand(blockId);
        var result = await commandService.UnblockAvailabilityAsync(command);
        return AppointmentActionResultAssembler.ToActionResult(result);
    }
}
