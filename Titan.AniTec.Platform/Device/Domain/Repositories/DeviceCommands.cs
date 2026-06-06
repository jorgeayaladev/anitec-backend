namespace Titan.AniTec.Platform.Device.Domain.Repositories;

public record RegisterDeviceTypeCommand(string Name, string? Description, string Category,
    string? Specifications, string? Metrics);

public record UpdateDeviceTypeCommand(int DeviceTypeId, string Name, string? Description, string Category,
    string? Specifications, string? Metrics);

public record DeleteDeviceTypeCommand(int DeviceTypeId);

public record RegisterDeviceCommand(int UserId, int DeviceTypeId, string Name, string SerialNumber,
    string? FirmwareVersion, int? CurrentLocationId, string? Configuration);

public record UpdateDeviceCommand(int UserId, int DeviceId, string Name, string SerialNumber,
    int DeviceTypeId, string? FirmwareVersion, int? CurrentLocationId, string? Configuration);

public record DeleteDeviceCommand(int UserId, int DeviceId);

public record AssignDeviceCommand(int UserId, int DeviceId, int AnimalId);

public record UnassignDeviceCommand(int UserId, int DeviceId);

public record PingDeviceCommand(int UserId, int DeviceId);

public record UpdateDeviceFirmwareCommand(int UserId, int DeviceId, string Version);

public record MarkDeviceMaintenanceCommand(int UserId, int DeviceId);

public record UpdateDeviceConfigurationCommand(int UserId, int DeviceId, string Configuration);
public record BatchAssignDevicesCommand(int UserId, IReadOnlyCollection<BatchAssignItem> Assignments);
public record BatchAssignItem(int DeviceId, int AnimalId);
public record UpdateReadingIntervalCommand(int UserId, int DeviceId, string Configuration);
public record UpdateAlertThresholdsCommand(int UserId, int DeviceId, string Configuration);
