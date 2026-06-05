using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;

public class Notification : IAuditableEntity
{
    public Notification(int userId, string type, string title, string message)
    {
        UserId = userId;
        Type = type;
        Title = title;
        Message = message;
        IsRead = false;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Type { get; private set; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public bool IsRead { get; private set; }
    public DateTimeOffset? ReadAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void MarkAsRead()
    {
        IsRead = true;
        ReadAt = DateTimeOffset.UtcNow;
    }
}
