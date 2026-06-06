using Titan.AniTec.Platform.Device.Application.CommandServices;
using Titan.AniTec.Platform.Device.Domain.Model;
using Titan.AniTec.Platform.Device.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Device.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Device.Application.Internal.QueryServices;

public class DeviceQueryService(
    IDeviceTypeRepository deviceTypeRepository,
    IDeviceRepository deviceRepository,
    IDeviceAssignmentRepository assignmentRepository,
    IDeviceAlertRepository alertRepository) : IDeviceQueryService
{
    public async Task<Result<IReadOnlyCollection<DeviceType>>> GetAllDeviceTypesAsync(GetAllDeviceTypesQuery query)
    {
        try
        {
            var types = await deviceTypeRepository.FindAllAsync();
            return Result<IReadOnlyCollection<DeviceType>>.Success(types);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<DeviceType>>.Failure(DeviceError.DeviceTypeNotFound);
        }
    }

    public async Task<Result<DeviceType>> GetDeviceTypeByIdAsync(GetDeviceTypeByIdQuery query)
    {
        try
        {
            var deviceType = await deviceTypeRepository.FindByIdAsync(query.DeviceTypeId);
            if (deviceType == null)
                return Result<DeviceType>.Failure(DeviceError.DeviceTypeNotFound);
            return Result<DeviceType>.Success(deviceType);
        }
        catch (Exception)
        {
            return Result<DeviceType>.Failure(DeviceError.DeviceTypeNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<DeviceType>>> GetDeviceTypesByCategoryAsync(GetDeviceTypesByCategoryQuery query)
    {
        try
        {
            var types = await deviceTypeRepository.FindByCategoryAsync(query.Category);
            return Result<IReadOnlyCollection<DeviceType>>.Success(types);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<DeviceType>>.Failure(DeviceError.DeviceTypeNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmDevice>>> GetAllDevicesAsync(GetAllDevicesQuery query)
    {
        try
        {
            var devices = await deviceRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<FarmDevice>>.Success(devices);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmDevice>>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<FarmDevice>> GetDeviceByIdAsync(GetDeviceByIdQuery query)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(query.DeviceId);
            if (device == null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceNotFound);
            return Result<FarmDevice>.Success(device);
        }
        catch (Exception)
        {
            return Result<FarmDevice>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmDevice>>> GetDevicesByTypeAsync(GetDevicesByTypeQuery query)
    {
        try
        {
            var devices = await deviceRepository.FindByTypeAsync(query.UserId, query.DeviceTypeId);
            return Result<IReadOnlyCollection<FarmDevice>>.Success(devices);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmDevice>>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmDevice>>> GetDevicesByStatusAsync(GetDevicesByStatusQuery query)
    {
        try
        {
            var devices = await deviceRepository.FindByStatusAsync(query.UserId, query.Status);
            return Result<IReadOnlyCollection<FarmDevice>>.Success(devices);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmDevice>>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmDevice>>> GetDevicesByAnimalAsync(GetDevicesByAnimalQuery query)
    {
        try
        {
            var devices = await deviceRepository.FindByAnimalIdAsync(query.UserId, query.AnimalId);
            return Result<IReadOnlyCollection<FarmDevice>>.Success(devices);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmDevice>>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmDevice>>> GetDevicesByLocationAsync(GetDevicesByLocationQuery query)
    {
        try
        {
            var devices = await deviceRepository.FindByLocationAsync(query.UserId, query.LocationId);
            return Result<IReadOnlyCollection<FarmDevice>>.Success(devices);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmDevice>>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmDevice>>> GetUnassignedDevicesAsync(GetUnassignedDevicesQuery query)
    {
        try
        {
            var devices = await deviceRepository.FindUnassignedAsync(query.UserId);
            return Result<IReadOnlyCollection<FarmDevice>>.Success(devices);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmDevice>>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmDevice>>> SearchDevicesAsync(SearchDevicesQuery query)
    {
        try
        {
            var devices = await deviceRepository.SearchAsync(query.UserId, query.Term);
            return Result<IReadOnlyCollection<FarmDevice>>.Success(devices);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmDevice>>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<DeviceAssignment>>> GetDeviceAssignmentHistoryAsync(GetDeviceAssignmentHistoryQuery query)
    {
        try
        {
            var history = await assignmentRepository.FindHistoryByDeviceIdAsync(query.DeviceId);
            return Result<IReadOnlyCollection<DeviceAssignment>>.Success(history);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<DeviceAssignment>>.Failure(DeviceError.DeviceAssignmentNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<DeviceAlert>>> GetDeviceAlertsAsync(GetDeviceAlertsQuery query)
    {
        try
        {
            var alerts = await alertRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<DeviceAlert>>.Success(alerts);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<DeviceAlert>>.Failure(DeviceError.DeviceAlertNotFound);
        }
    }

    public async Task<Result<string>> GetDeviceStatusAsync(GetDeviceStatusQuery query)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(query.DeviceId);
            if (device == null)
                return Result<string>.Failure(DeviceError.DeviceNotFound);
            return Result<string>.Success(device.Status);
        }
        catch (Exception)
        {
            return Result<string>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<double?>> GetDeviceBatteryAsync(GetDeviceBatteryQuery query)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(query.DeviceId);
            if (device == null)
                return Result<double?>.Failure(DeviceError.DeviceNotFound);
            return Result<double?>.Success(device.BatteryLevel);
        }
        catch (Exception)
        {
            return Result<double?>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<double?>> GetDeviceSignalAsync(GetDeviceSignalQuery query)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(query.DeviceId);
            if (device == null)
                return Result<double?>.Failure(DeviceError.DeviceNotFound);
            return Result<double?>.Success(device.SignalStrength);
        }
        catch (Exception)
        {
            return Result<double?>.Failure(DeviceError.DeviceNotFound);
        }
    }

    public async Task<Result<DateTime?>> GetDeviceLastReadingAsync(GetDeviceLastReadingQuery query)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(query.DeviceId);
            if (device == null)
                return Result<DateTime?>.Failure(DeviceError.DeviceNotFound);
            return Result<DateTime?>.Success(device.LastReadingAt);
        }
        catch (Exception)
        {
            return Result<DateTime?>.Failure(DeviceError.DeviceNotFound);
        }
    }
}
