using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class Vaccination : IAuditableEntity
{
    public Vaccination(int farmId, int animalId, string vaccineName, DateTime applicationDate,
        string? batchNumber, string? applicationRoute, string? dosage, string? appliedBy,
        DateTime? nextDoseDate, string? notes)
    {
        FarmId = farmId;
        AnimalId = animalId;
        VaccineName = vaccineName;
        ApplicationDate = applicationDate;
        BatchNumber = batchNumber;
        ApplicationRoute = applicationRoute;
        Dosage = dosage;
        AppliedBy = appliedBy;
        NextDoseDate = nextDoseDate;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int AnimalId { get; private set; }
    public string VaccineName { get; private set; }
    public DateTime ApplicationDate { get; private set; }
    public string? BatchNumber { get; private set; }
    public string? ApplicationRoute { get; private set; }
    public string? Dosage { get; private set; }
    public string? AppliedBy { get; private set; }
    public DateTime? NextDoseDate { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Vaccination UpdateDetails(string vaccineName, DateTime applicationDate,
        string? batchNumber, string? applicationRoute, string? dosage, string? appliedBy,
        DateTime? nextDoseDate, string? notes)
    {
        VaccineName = vaccineName;
        ApplicationDate = applicationDate;
        BatchNumber = batchNumber;
        ApplicationRoute = applicationRoute;
        Dosage = dosage;
        AppliedBy = appliedBy;
        NextDoseDate = nextDoseDate;
        Notes = notes;
        return this;
    }
}
