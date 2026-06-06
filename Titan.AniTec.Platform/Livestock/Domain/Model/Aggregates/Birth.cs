namespace Titan.AniTec.Platform.Livestock.Domain.Model.Aggregates;

public class Birth
{
    public Birth(int farmId, int motherId, DateTime birthDate, int offspringCount, int? fatherId, string? notes)
    {
        FarmId = farmId;
        MotherId = motherId;
        BirthDate = birthDate;
        OffspringCount = offspringCount;
        FatherId = fatherId;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int MotherId { get; private set; }
    public int? FatherId { get; private set; }
    public DateTime BirthDate { get; private set; }
    public int OffspringCount { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public Birth UpdateDetails(DateTime birthDate, int offspringCount, int? fatherId, string? notes)
    {
        BirthDate = birthDate;
        OffspringCount = offspringCount;
        FatherId = fatherId;
        Notes = notes;
        return this;
    }
}
