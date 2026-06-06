namespace Titan.AniTec.Platform.Device.Interfaces.Resources;

public record DeviceTypeResource(
    int Id, string Name, string? Description, string Category,
    string? Specifications, string? Metrics);

public record CreateDeviceTypeResource(
    string Name, string? Description, string Category,
    string? Specifications, string? Metrics);

public record UpdateDeviceTypeResource(
    string Name, string? Description, string Category,
    string? Specifications, string? Metrics);

public record DeviceResource(
    int Id, int FarmId, int DeviceTypeId, string Name, string SerialNumber,
    string Status, string? FirmwareVersion, DateTime? LastPingAt,
    double? BatteryLevel, double? SignalStrength, DateTime? LastReadingAt,
    int? CurrentAnimalId, int? CurrentLocationId, string? Configuration);

public record CreateDeviceResource(
    int DeviceTypeId, string Name, string SerialNumber,
    string? FirmwareVersion, int? CurrentLocationId, string? Configuration);

public record UpdateDeviceResource(
    string Name, string SerialNumber, int DeviceTypeId,
    string? FirmwareVersion, int? CurrentLocationId, string? Configuration);

public record DeviceAssignmentResource(
    int Id, int DeviceId, int AnimalId, int FarmId,
    DateTime AssignedAt, DateTime? UnassignedAt, bool IsActive);

public record AssignDeviceResource(int AnimalId);

public record UpdateDeviceFirmwareResource(string Version);

public record UpdateDeviceConfigurationResource(string Configuration);

public record DeviceAlertResource(
    int Id, int DeviceId, int FarmId, string AlertType, string Description,
    bool IsResolved, DateTime? ResolvedAt);
public record BatchAssignDevicesResource(IReadOnlyCollection<BatchAssignItemResource> Assignments);
public record BatchAssignItemResource(int DeviceId, int AnimalId);
public record UpdateReadingIntervalResource(string Configuration);
public record UpdateAlertThresholdsResource(string Configuration);
public record ConfigTemplateResource(int Id, string Name, string? Description, string Configuration);
public record CreateConfigTemplateResource(string Name, string? Description, string Configuration);
