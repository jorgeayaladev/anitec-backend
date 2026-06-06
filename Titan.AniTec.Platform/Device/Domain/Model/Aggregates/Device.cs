using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Device.Domain.Model.Aggregates;

public class FarmDevice : IAuditableEntity
{
    public FarmDevice(int farmId, int deviceTypeId, string name, string serialNumber)
    {
        FarmId = farmId;
        DeviceTypeId = deviceTypeId;
        Name = name;
        SerialNumber = serialNumber;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int DeviceTypeId { get; private set; }
    public string Name { get; private set; }
    public string SerialNumber { get; private set; }
    public string Status { get; private set; } = "active";
    public string? FirmwareVersion { get; private set; }
    public DateTime? LastPingAt { get; private set; }
    public double? BatteryLevel { get; private set; }
    public double? SignalStrength { get; private set; }
    public DateTime? LastReadingAt { get; private set; }
    public int? CurrentAnimalId { get; private set; }
    public int? CurrentLocationId { get; private set; }
    public string? Configuration { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public FarmDevice UpdateDetails(string name, string serialNumber, int deviceTypeId,
        string? firmwareVersion, int? currentLocationId, string? configuration)
    {
        Name = name;
        SerialNumber = serialNumber;
        DeviceTypeId = deviceTypeId;
        FirmwareVersion = firmwareVersion;
        CurrentLocationId = currentLocationId;
        Configuration = configuration;
        return this;
    }

    public FarmDevice UpdateStatus(string status)
    {
        Status = status;
        return this;
    }

    public FarmDevice MarkAsMaintenance()
    {
        Status = "maintenance";
        return this;
    }

    public FarmDevice AssignToAnimal(int animalId)
    {
        CurrentAnimalId = animalId;
        Status = "active";
        return this;
    }

    public FarmDevice UnassignFromAnimal()
    {
        CurrentAnimalId = null;
        return this;
    }

    public FarmDevice UpdateBattery(double? batteryLevel)
    {
        BatteryLevel = batteryLevel;
        return this;
    }

    public FarmDevice UpdateSignal(double? signalStrength)
    {
        SignalStrength = signalStrength;
        return this;
    }

    public FarmDevice Ping()
    {
        LastPingAt = DateTime.UtcNow;
        return this;
    }

    public FarmDevice UpdateFirmware(string version)
    {
        FirmwareVersion = version;
        return this;
    }

    public FarmDevice UpdateReadingInterval(string configuration)
    {
        Configuration = configuration;
        return this;
    }

    public FarmDevice UpdateAlertThresholds(string configuration)
    {
        Configuration = configuration;
        return this;
    }
}
