using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Device.Domain.Model.Aggregates;

public class DeviceAlert : IAuditableEntity
{
    public DeviceAlert(int deviceId, int farmId, string alertType, string description)
    {
        DeviceId = deviceId;
        FarmId = farmId;
        AlertType = alertType;
        Description = description;
    }

    public int Id { get; private set; }
    public int DeviceId { get; private set; }
    public int FarmId { get; private set; }
    public string AlertType { get; private set; }
    public string Description { get; private set; }
    public bool IsResolved { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public DeviceAlert Resolve()
    {
        IsResolved = true;
        ResolvedAt = DateTime.UtcNow;
        return this;
    }
}
