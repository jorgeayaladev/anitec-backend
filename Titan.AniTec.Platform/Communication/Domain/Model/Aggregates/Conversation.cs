namespace Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;

public class Conversation
{
    public Conversation(int farmId, string title, string participants)
    {
        FarmId = farmId;
        Title = title;
        Participants = participants;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string Title { get; private set; }
    public string Participants { get; private set; }
    public bool IsArchived { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public Conversation UpdateDetails(string title, string participants)
    {
        Title = title;
        Participants = participants;
        return this;
    }

    public Conversation MarkAsArchived()
    {
        IsArchived = true;
        return this;
    }
}
