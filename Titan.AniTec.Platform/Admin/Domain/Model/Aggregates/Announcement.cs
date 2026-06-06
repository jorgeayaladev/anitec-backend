namespace Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;

public class Announcement
{
    public Announcement(string title, string content, string? severity)
    {
        Title = title;
        Content = content;
        Severity = severity ?? "info";
        IsActive = true;
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Severity { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public Announcement Update(string title, string content, string? severity)
    {
        Title = title;
        Content = content;
        Severity = severity ?? "info";
        return this;
    }

    public Announcement Deactivate()
    {
        IsActive = false;
        return this;
    }
}
