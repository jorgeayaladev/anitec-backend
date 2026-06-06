namespace Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;

public class AuditLog
{
    public AuditLog(int userId, string action, string entityType, int entityId, string? details)
    {
        UserId = userId;
        Action = action;
        EntityType = entityType;
        EntityId = entityId;
        Details = details;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Action { get; private set; }
    public string EntityType { get; private set; }
    public int EntityId { get; private set; }
    public string? Details { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
}
