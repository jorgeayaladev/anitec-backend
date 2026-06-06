using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Appointment.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Titan.AniTec.Platform.Appointment.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AppointmentRepository(AppDbContext context)
    : BaseRepository<VeterinaryAppointment>(context), IAppointmentRepository
{
    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindByFarmIdAsync(int farmId,
        string? status = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = Context.Set<VeterinaryAppointment>().Where(a => a.FarmId == farmId);
        if (!string.IsNullOrEmpty(status))
            query = query.Where(a => a.Status == status);
        if (startDate.HasValue)
            query = query.Where(a => a.ScheduledAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(a => a.ScheduledAt <= endDate.Value);
        return await query.OrderByDescending(a => a.ScheduledAt).ToListAsync();
    }

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindByVeterinarianIdAsync(int veterinarianId,
        string? status = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = Context.Set<VeterinaryAppointment>().Where(a => a.VeterinarianId == veterinarianId);
        if (!string.IsNullOrEmpty(status))
            query = query.Where(a => a.Status == status);
        if (startDate.HasValue)
            query = query.Where(a => a.ScheduledAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(a => a.ScheduledAt <= endDate.Value);
        return await query.OrderByDescending(a => a.ScheduledAt).ToListAsync();
    }

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindByFarmerIdAsync(int farmerId,
        string? status = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = Context.Set<VeterinaryAppointment>().Where(a => a.FarmerId == farmerId);
        if (!string.IsNullOrEmpty(status))
            query = query.Where(a => a.Status == status);
        if (startDate.HasValue)
            query = query.Where(a => a.ScheduledAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(a => a.ScheduledAt <= endDate.Value);
        return await query.OrderByDescending(a => a.ScheduledAt).ToListAsync();
    }

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindByAnimalIdAsync(int animalId)
        => await Context.Set<VeterinaryAppointment>()
            .Where(a => a.AnimalId == animalId)
            .OrderByDescending(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindByDateRangeAsync(int userId, DateTime start, DateTime end)
        => await Context.Set<VeterinaryAppointment>()
            .Where(a => a.FarmId == userId && a.ScheduledAt >= start && a.ScheduledAt <= end)
            .OrderBy(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindByDateAsync(int userId, DateTime date)
        => await Context.Set<VeterinaryAppointment>()
            .Where(a => a.FarmId == userId && a.ScheduledAt.Date == date.Date)
            .OrderBy(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindByWeekAsync(int userId, DateTime weekStart)
    {
        var weekEnd = weekStart.AddDays(7);
        return await Context.Set<VeterinaryAppointment>()
            .Where(a => a.FarmId == userId && a.ScheduledAt >= weekStart && a.ScheduledAt < weekEnd)
            .OrderBy(a => a.ScheduledAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindUpcomingAsync(int userId)
        => await Context.Set<VeterinaryAppointment>()
            .Where(a => a.FarmId == userId && a.ScheduledAt >= DateTime.UtcNow
                     && a.Status != "canceled" && a.Status != "completed")
            .OrderBy(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindPastAsync(int userId)
        => await Context.Set<VeterinaryAppointment>()
            .Where(a => a.FarmId == userId && a.ScheduledAt < DateTime.UtcNow)
            .OrderByDescending(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindByStatusAsync(int userId, string status)
        => await Context.Set<VeterinaryAppointment>()
            .Where(a => a.FarmId == userId && a.Status == status)
            .OrderByDescending(a => a.ScheduledAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<VeterinaryAppointment>> FindByTypeAsync(int userId, string type)
        => await Context.Set<VeterinaryAppointment>()
            .Where(a => a.FarmId == userId && a.AppointmentType == type)
            .OrderByDescending(a => a.ScheduledAt)
            .ToListAsync();
}

public class AppointmentReminderRepository(AppDbContext context)
    : BaseRepository<AppointmentReminder>(context), IAppointmentReminderRepository
{
    public async Task<IReadOnlyCollection<AppointmentReminder>> FindByAppointmentIdAsync(int appointmentId)
        => await Context.Set<AppointmentReminder>()
            .Where(r => r.AppointmentId == appointmentId)
            .OrderBy(r => r.RemindAtMinutesBefore)
            .ToListAsync();
}

public class AppointmentFollowUpRepository(AppDbContext context)
    : BaseRepository<AppointmentFollowUp>(context), IAppointmentFollowUpRepository
{
    public async Task<IReadOnlyCollection<AppointmentFollowUp>> FindByAppointmentIdAsync(int appointmentId)
        => await Context.Set<AppointmentFollowUp>()
            .Where(f => f.AppointmentId == appointmentId)
            .OrderByDescending(f => f.ScheduledAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<AppointmentFollowUp>> FindPendingAsync(int farmId)
        => await Context.Set<VeterinaryAppointment>()
            .Where(a => a.FarmId == farmId)
            .Join(Context.Set<AppointmentFollowUp>(),
                a => a.Id, f => f.AppointmentId,
                (a, f) => f)
            .Where(f => f.Status == "pending")
            .OrderBy(f => f.ScheduledAt)
            .ToListAsync();
}

public class AppointmentNoteRepository(AppDbContext context)
    : BaseRepository<AppointmentNote>(context), IAppointmentNoteRepository
{
    public async Task<IReadOnlyCollection<AppointmentNote>> FindByAppointmentIdAsync(int appointmentId)
        => await Context.Set<AppointmentNote>()
            .Where(n => n.AppointmentId == appointmentId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
}

public class VeterinarianAvailabilityRepository(AppDbContext context)
    : BaseRepository<VeterinarianAvailability>(context), IVeterinarianAvailabilityRepository
{
    public async Task<IReadOnlyCollection<VeterinarianAvailability>> FindByVeterinarianIdAsync(int veterinarianId)
        => await Context.Set<VeterinarianAvailability>()
            .Where(v => v.VeterinarianId == veterinarianId)
            .OrderBy(v => v.DayOfWeek).ThenBy(v => v.StartTime)
            .ToListAsync();

    public async Task<VeterinarianAvailability?> FindByVeterinarianAndDayAsync(int veterinarianId, int dayOfWeek)
        => await Context.Set<VeterinarianAvailability>()
            .FirstOrDefaultAsync(v => v.VeterinarianId == veterinarianId && v.DayOfWeek == dayOfWeek);
}

public class AvailabilityBlockRepository(AppDbContext context)
    : BaseRepository<AvailabilityBlock>(context), IAvailabilityBlockRepository
{
    public async Task<IReadOnlyCollection<AvailabilityBlock>> FindByVeterinarianIdAsync(int veterinarianId,
        DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = Context.Set<AvailabilityBlock>()
            .Where(b => b.VeterinarianId == veterinarianId);
        if (startDate.HasValue)
            query = query.Where(b => b.StartAt >= startDate.Value || b.EndAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(b => b.StartAt <= endDate.Value);
        return await query.OrderBy(b => b.StartAt).ToListAsync();
    }

    public async Task<IReadOnlyCollection<AvailabilityBlock>> FindOverlappingAsync(int veterinarianId,
        DateTime startAt, DateTime endAt)
        => await Context.Set<AvailabilityBlock>()
            .Where(b => b.VeterinarianId == veterinarianId
                     && b.StartAt < endAt && b.EndAt > startAt)
            .ToListAsync();
}
