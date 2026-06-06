namespace Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;

public class Mating
{
    public Mating(int farmId, int femaleId, int maleId, DateTime matingDate, string? notes)
    {
        FarmId = farmId;
        FemaleId = femaleId;
        MaleId = maleId;
        MatingDate = matingDate;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int FemaleId { get; private set; }
    public int MaleId { get; private set; }
    public DateTime MatingDate { get; private set; }
    public string? Result { get; private set; }
    public string? Notes { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Mating UpdateDetails(DateTime matingDate, int maleId, string? notes)
    {
        MatingDate = matingDate;
        MaleId = maleId;
        Notes = notes;
        return this;
    }

    public Mating ConfirmResult(string result)
    {
        Result = result;
        ConfirmedAt = DateTime.UtcNow;
        return this;
    }
}
