namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class Diagnosis
{
    public Diagnosis(int farmId, int animalId, string condition, string? description,
        string? severity, string? diagnosedBy, DateTime diagnosedDate, int? diseaseId)
    {
        FarmId = farmId;
        AnimalId = animalId;
        Condition = condition;
        Description = description;
        Severity = severity;
        DiagnosedBy = diagnosedBy;
        DiagnosedDate = diagnosedDate;
        DiseaseId = diseaseId;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int AnimalId { get; private set; }
    public string Condition { get; private set; }
    public string? Description { get; private set; }
    public string? Severity { get; private set; }
    public string? DiagnosedBy { get; private set; }
    public DateTime DiagnosedDate { get; private set; }
    public int? DiseaseId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public Diagnosis UpdateDetails(string condition, string? description,
        string? severity, string? diagnosedBy, DateTime diagnosedDate, int? diseaseId)
    {
        Condition = condition;
        Description = description;
        Severity = severity;
        DiagnosedBy = diagnosedBy;
        DiagnosedDate = diagnosedDate;
        DiseaseId = diseaseId;
        return this;
    }
}
