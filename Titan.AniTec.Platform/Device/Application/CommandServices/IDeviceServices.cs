using Titan.AniTec.Platform.Device.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Device.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Device.Application.CommandServices;

public interface IDeviceCommandService
{
    Task<Result<DeviceType>> RegisterDeviceTypeAsync(RegisterDeviceTypeCommand command);
    Task<Result<DeviceType>> UpdateDeviceTypeAsync(UpdateDeviceTypeCommand command);
    Task<Result> DeleteDeviceTypeAsync(DeleteDeviceTypeCommand command);
    Task<Result<FarmDevice>> RegisterDeviceAsync(RegisterDeviceCommand command);
    Task<Result<FarmDevice>> UpdateDeviceAsync(UpdateDeviceCommand command);
    Task<Result> DeleteDeviceAsync(DeleteDeviceCommand command);
    Task<Result> AssignDeviceAsync(AssignDeviceCommand command);
    Task<Result> UnassignDeviceAsync(UnassignDeviceCommand command);
    Task<Result<FarmDevice>> PingDeviceAsync(PingDeviceCommand command);
    Task<Result<FarmDevice>> UpdateDeviceFirmwareAsync(UpdateDeviceFirmwareCommand command);
    Task<Result<FarmDevice>> MarkDeviceMaintenanceAsync(MarkDeviceMaintenanceCommand command);
    Task<Result<FarmDevice>> UpdateDeviceConfigurationAsync(UpdateDeviceConfigurationCommand command);
    Task<Result> BatchAssignDevicesAsync(BatchAssignDevicesCommand command);
    Task<Result<FarmDevice>> UpdateReadingIntervalAsync(UpdateReadingIntervalCommand command);
    Task<Result<FarmDevice>> UpdateAlertThresholdsAsync(UpdateAlertThresholdsCommand command);
}

public interface IDeviceQueryService
{
    Task<Result<IReadOnlyCollection<DeviceType>>> GetAllDeviceTypesAsync(GetAllDeviceTypesQuery query);
    Task<Result<DeviceType>> GetDeviceTypeByIdAsync(GetDeviceTypeByIdQuery query);
    Task<Result<IReadOnlyCollection<DeviceType>>> GetDeviceTypesByCategoryAsync(GetDeviceTypesByCategoryQuery query);
    Task<Result<IReadOnlyCollection<FarmDevice>>> GetAllDevicesAsync(GetAllDevicesQuery query);
    Task<Result<FarmDevice>> GetDeviceByIdAsync(GetDeviceByIdQuery query);
    Task<Result<IReadOnlyCollection<FarmDevice>>> GetDevicesByTypeAsync(GetDevicesByTypeQuery query);
    Task<Result<IReadOnlyCollection<FarmDevice>>> GetDevicesByStatusAsync(GetDevicesByStatusQuery query);
    Task<Result<IReadOnlyCollection<FarmDevice>>> GetDevicesByAnimalAsync(GetDevicesByAnimalQuery query);
    Task<Result<IReadOnlyCollection<FarmDevice>>> GetDevicesByLocationAsync(GetDevicesByLocationQuery query);
    Task<Result<IReadOnlyCollection<FarmDevice>>> GetUnassignedDevicesAsync(GetUnassignedDevicesQuery query);
    Task<Result<IReadOnlyCollection<FarmDevice>>> SearchDevicesAsync(SearchDevicesQuery query);
    Task<Result<IReadOnlyCollection<DeviceAssignment>>> GetDeviceAssignmentHistoryAsync(GetDeviceAssignmentHistoryQuery query);
    Task<Result<IReadOnlyCollection<DeviceAlert>>> GetDeviceAlertsAsync(GetDeviceAlertsQuery query);
    Task<Result<string>> GetDeviceStatusAsync(GetDeviceStatusQuery query);
    Task<Result<double?>> GetDeviceBatteryAsync(GetDeviceBatteryQuery query);
    Task<Result<double?>> GetDeviceSignalAsync(GetDeviceSignalQuery query);
    Task<Result<DateTime?>> GetDeviceLastReadingAsync(GetDeviceLastReadingQuery query);
}
