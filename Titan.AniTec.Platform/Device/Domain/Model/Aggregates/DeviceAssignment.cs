using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Device.Domain.Model.Aggregates;

public class DeviceAssignment : IAuditableEntity
{
    public DeviceAssignment(int deviceId, int animalId, int farmId)
    {
        DeviceId = deviceId;
        AnimalId = animalId;
        FarmId = farmId;
    }

    public int Id { get; private set; }
    public int DeviceId { get; private set; }
    public int AnimalId { get; private set; }
    public int FarmId { get; private set; }
    public DateTime AssignedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UnassignedAt { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public DeviceAssignment Unassign()
    {
        IsActive = false;
        UnassignedAt = DateTime.UtcNow;
        return this;
    }
}
