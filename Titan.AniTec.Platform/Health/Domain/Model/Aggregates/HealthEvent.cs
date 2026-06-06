namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class HealthEvent
{
    public HealthEvent(int farmId, int? animalId, string title, string? description,
        string eventType, DateTime scheduledDate, string? notes)
    {
        FarmId = farmId;
        AnimalId = animalId;
        Title = title;
        Description = description;
        EventType = eventType;
        ScheduledDate = scheduledDate;
        Notes = notes;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int? AnimalId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string EventType { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string? CompletedBy { get; private set; }
    public bool IsPostponed { get; private set; }
    public DateTime? PostponedTo { get; private set; }
    public string? Notes { get; private set; }

    public HealthEvent UpdateDetails(string title, string? description, string eventType,
        DateTime scheduledDate, int? animalId, string? notes)
    {
        Title = title;
        Description = description;
        EventType = eventType;
        ScheduledDate = scheduledDate;
        AnimalId = animalId;
        Notes = notes;
        return this;
    }

    public HealthEvent MarkAsCompleted(string? completedBy)
    {
        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
        CompletedBy = completedBy;
        return this;
    }

    public HealthEvent Postpone(DateTime postponedTo)
    {
        IsPostponed = true;
        PostponedTo = postponedTo;
        return this;
    }
}
