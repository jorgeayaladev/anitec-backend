namespace Titan.AniTec.Platform.Device.Domain.Repositories;

public record GetAllDeviceTypesQuery;

public record GetDeviceTypeByIdQuery(int DeviceTypeId);

public record GetDeviceTypesByCategoryQuery(string Category);

public record GetAllDevicesQuery(int UserId);

public record GetDeviceByIdQuery(int UserId, int DeviceId);

public record GetDevicesByTypeQuery(int UserId, int DeviceTypeId);

public record GetDevicesByStatusQuery(int UserId, string Status);

public record GetDevicesByAnimalQuery(int UserId, int AnimalId);

public record GetDevicesByLocationQuery(int UserId, int LocationId);

public record GetUnassignedDevicesQuery(int UserId);

public record SearchDevicesQuery(int UserId, string Term);

public record GetDeviceAssignmentHistoryQuery(int UserId, int DeviceId);

public record GetDeviceAlertsQuery(int UserId);
public record GetDeviceStatusQuery(int UserId, int DeviceId);
public record GetDeviceBatteryQuery(int UserId, int DeviceId);
public record GetDeviceSignalQuery(int UserId, int DeviceId);
public record GetDeviceLastReadingQuery(int UserId, int DeviceId);
