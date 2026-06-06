using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Health.Domain.Model.Aggregates;

public class HealthAlert : IAuditableEntity
{
    public HealthAlert(int farmId, int animalId, string alertType, string description)
    {
        FarmId = farmId;
        AnimalId = animalId;
        AlertType = alertType;
        Description = description;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int AnimalId { get; private set; }
    public string AlertType { get; private set; }
    public string Description { get; private set; }
    public bool IsResolved { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public string? ResolvedBy { get; private set; }
    public string? Notes { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public HealthAlert Resolve(string? resolvedBy, string? notes)
    {
        IsResolved = true;
        ResolvedAt = DateTime.UtcNow;
        ResolvedBy = resolvedBy;
        Notes = notes;
        return this;
    }

    public HealthAlert Acknowledge(string? acknowledgedBy)
    {
        ResolvedBy = acknowledgedBy;
        Notes = "Acknowledged";
        return this;
    }
}
