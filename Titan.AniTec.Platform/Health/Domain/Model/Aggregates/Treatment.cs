namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class Treatment
{
    public Treatment(int farmId, int animalId, DateTime treatmentDate, string diagnosis,
        string? medicationName, string? dosage, string? administrationRoute,
        int? durationDays, int? withdrawalPeriodDays, string? treatedBy, string? notes)
    {
        FarmId = farmId;
        AnimalId = animalId;
        TreatmentDate = treatmentDate;
        Diagnosis = diagnosis;
        MedicationName = medicationName;
        Dosage = dosage;
        AdministrationRoute = administrationRoute;
        DurationDays = durationDays;
        WithdrawalPeriodDays = withdrawalPeriodDays;
        TreatedBy = treatedBy;
        Notes = notes;
        Status = "pending";
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int AnimalId { get; private set; }
    public DateTime TreatmentDate { get; private set; }
    public string Diagnosis { get; private set; }
    public string? MedicationName { get; private set; }
    public string? Dosage { get; private set; }
    public string? AdministrationRoute { get; private set; }
    public int? DurationDays { get; private set; }
    public int? WithdrawalPeriodDays { get; private set; }
    public string? TreatedBy { get; private set; }
    public string? Notes { get; private set; }
    public string Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public Treatment UpdateDetails(DateTime treatmentDate, string diagnosis,
        string? medicationName, string? dosage, string? administrationRoute,
        int? durationDays, int? withdrawalPeriodDays, string? treatedBy, string? notes)
    {
        TreatmentDate = treatmentDate;
        Diagnosis = diagnosis;
        MedicationName = medicationName;
        Dosage = dosage;
        AdministrationRoute = administrationRoute;
        DurationDays = durationDays;
        WithdrawalPeriodDays = withdrawalPeriodDays;
        TreatedBy = treatedBy;
        Notes = notes;
        return this;
    }

    public Treatment MarkAsStarted()
    {
        Status = "active";
        return this;
    }

    public Treatment MarkAsCompleted()
    {
        Status = "completed";
        return this;
    }

    public Treatment MarkAsCancelled()
    {
        Status = "cancelled";
        return this;
    }
}
