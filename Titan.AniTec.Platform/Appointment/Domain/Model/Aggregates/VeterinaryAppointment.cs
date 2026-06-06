using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Appointment.Domain.Model.Aggregates;

public class VeterinaryAppointment : IAuditableEntity
{
    public VeterinaryAppointment(int farmId, int veterinarianId, int farmerId, string appointmentType,
        DateTime scheduledAt, int durationMinutes, int? animalId = null, string? location = null,
        string? notes = null)
    {
        FarmId = farmId;
        VeterinarianId = veterinarianId;
        FarmerId = farmerId;
        AnimalId = animalId;
        AppointmentType = appointmentType;
        Status = "pending";
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        Location = location;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int VeterinarianId { get; private set; }
    public int FarmerId { get; private set; }
    public int? AnimalId { get; private set; }
    public string AppointmentType { get; private set; }
    public string Status { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public int DurationMinutes { get; private set; }
    public string? Location { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public VeterinaryAppointment UpdateDetails(string appointmentType, DateTime scheduledAt, int durationMinutes,
        int? animalId, string? location, string? notes)
    {
        AppointmentType = appointmentType;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        AnimalId = animalId;
        Location = location;
        Notes = notes;
        return this;
    }

    public VeterinaryAppointment Confirm()
    {
        Status = "confirmed";
        return this;
    }

    public VeterinaryAppointment Cancel()
    {
        Status = "canceled";
        return this;
    }

    public VeterinaryAppointment Complete()
    {
        Status = "completed";
        return this;
    }

    public VeterinaryAppointment UpdateNotes(string notes)
    {
        Notes = notes;
        return this;
    }
}
