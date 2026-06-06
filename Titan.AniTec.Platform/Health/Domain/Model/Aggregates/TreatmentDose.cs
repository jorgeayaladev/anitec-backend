namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class TreatmentDose
{
    public TreatmentDose(int treatmentId, DateTime administeredDate, string? administeredBy,
        string? dosage, string? notes)
    {
        TreatmentId = treatmentId;
        AdministeredDate = administeredDate;
        AdministeredBy = administeredBy;
        Dosage = dosage;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int TreatmentId { get; private set; }
    public DateTime AdministeredDate { get; private set; }
    public string? AdministeredBy { get; private set; }
    public string? Dosage { get; private set; }
    public string? Notes { get; private set; }
}
