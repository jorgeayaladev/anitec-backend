namespace Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;

public class Weaning
{
    public Weaning(int farmId, int calfId, int motherId, DateTime weaningDate, double? weight, string? notes)
    {
        FarmId = farmId;
        CalfId = calfId;
        MotherId = motherId;
        WeaningDate = weaningDate;
        Weight = weight;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int CalfId { get; private set; }
    public int MotherId { get; private set; }
    public DateTime WeaningDate { get; private set; }
    public double? Weight { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public Weaning UpdateDetails(DateTime weaningDate, double? weight, string? notes)
    {
        WeaningDate = weaningDate;
        Weight = weight;
        Notes = notes;
        return this;
    }
}
