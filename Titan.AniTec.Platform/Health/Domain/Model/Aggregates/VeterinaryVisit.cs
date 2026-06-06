namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class VeterinaryVisit
{
    public VeterinaryVisit(int farmId, int animalId, DateTime visitDate, string vetName,
        string reason, string? diagnosis, string? recommendations,
        DateTime? followUpDate, decimal? cost, string? notes)
    {
        FarmId = farmId;
        AnimalId = animalId;
        VisitDate = visitDate;
        VetName = vetName;
        Reason = reason;
        Diagnosis = diagnosis;
        Recommendations = recommendations;
        FollowUpDate = followUpDate;
        Cost = cost;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int AnimalId { get; private set; }
    public DateTime VisitDate { get; private set; }
    public string VetName { get; private set; }
    public string Reason { get; private set; }
    public string? Diagnosis { get; private set; }
    public string? Recommendations { get; private set; }
    public DateTime? FollowUpDate { get; private set; }
    public decimal? Cost { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public VeterinaryVisit UpdateDetails(DateTime visitDate, string vetName,
        string reason, string? diagnosis, string? recommendations,
        DateTime? followUpDate, decimal? cost, string? notes)
    {
        VisitDate = visitDate;
        VetName = vetName;
        Reason = reason;
        Diagnosis = diagnosis;
        Recommendations = recommendations;
        FollowUpDate = followUpDate;
        Cost = cost;
        Notes = notes;
        return this;
    }
}
