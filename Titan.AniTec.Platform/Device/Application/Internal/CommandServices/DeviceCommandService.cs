using Titan.AniTec.Platform.Device.Application.CommandServices;
using Titan.AniTec.Platform.Device.Domain.Model;
using Titan.AniTec.Platform.Device.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Device.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Device.Application.Internal.CommandServices;

public class DeviceCommandService(
    IDeviceTypeRepository deviceTypeRepository,
    IDeviceRepository deviceRepository,
    IDeviceAssignmentRepository assignmentRepository,
    IUnitOfWork unitOfWork) : IDeviceCommandService
{
    public async Task<Result<DeviceType>> RegisterDeviceTypeAsync(RegisterDeviceTypeCommand command)
    {
        try
        {
            var existing = await deviceTypeRepository.FindByNameAsync(command.Name);
            if (existing != null)
                return Result<DeviceType>.Failure(DeviceError.DeviceTypeAlreadyExists);

            var deviceType = new DeviceType(command.Name, command.Description, command.Category,
                command.Specifications, command.Metrics);
            await deviceTypeRepository.AddAsync(deviceType);
            await unitOfWork.CompleteAsync();
            return Result<DeviceType>.Success(deviceType);
        }
        catch (Exception)
        {
            return Result<DeviceType>.Failure(DeviceError.InvalidDeviceTypeData);
        }
    }

    public async Task<Result<DeviceType>> UpdateDeviceTypeAsync(UpdateDeviceTypeCommand command)
    {
        try
        {
            var deviceType = await deviceTypeRepository.FindByIdAsync(command.DeviceTypeId);
            if (deviceType == null)
                return Result<DeviceType>.Failure(DeviceError.DeviceTypeNotFound);

            deviceType.UpdateDetails(command.Name, command.Description, command.Category,
                command.Specifications, command.Metrics);
            await unitOfWork.CompleteAsync();
            return Result<DeviceType>.Success(deviceType);
        }
        catch (Exception)
        {
            return Result<DeviceType>.Failure(DeviceError.InvalidDeviceTypeData);
        }
    }

    public async Task<Result> DeleteDeviceTypeAsync(DeleteDeviceTypeCommand command)
    {
        try
        {
            var deviceType = await deviceTypeRepository.FindByIdAsync(command.DeviceTypeId);
            if (deviceType == null)
                return Result.Failure(DeviceError.DeviceTypeNotFound);

            deviceTypeRepository.Remove(deviceType);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(DeviceError.InvalidDeviceTypeData);
        }
    }

    public async Task<Result<FarmDevice>> RegisterDeviceAsync(RegisterDeviceCommand command)
    {
        try
        {
            var existing = await deviceRepository.FindBySerialNumberAsync(command.UserId, command.SerialNumber);
            if (existing != null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceAlreadyExists);

            var deviceType = await deviceTypeRepository.FindByIdAsync(command.DeviceTypeId);
            if (deviceType == null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceTypeNotFound);

            var device = new FarmDevice(command.UserId, command.DeviceTypeId, command.Name, command.SerialNumber);
            if (!string.IsNullOrEmpty(command.FirmwareVersion))
                device.UpdateFirmware(command.FirmwareVersion);
            if (command.CurrentLocationId.HasValue)
                device.UpdateDetails(device.Name, device.SerialNumber, device.DeviceTypeId,
                    device.FirmwareVersion, command.CurrentLocationId, command.Configuration);
            if (!string.IsNullOrEmpty(command.Configuration))
                device.UpdateReadingInterval(command.Configuration);

            await deviceRepository.AddAsync(device);
            await unitOfWork.CompleteAsync();
            return Result<FarmDevice>.Success(device);
        }
        catch (Exception)
        {
            return Result<FarmDevice>.Failure(DeviceError.InvalidDeviceData);
        }
    }

    public async Task<Result<FarmDevice>> UpdateDeviceAsync(UpdateDeviceCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceNotFound);

            device.UpdateDetails(command.Name, command.SerialNumber, command.DeviceTypeId,
                command.FirmwareVersion, command.CurrentLocationId, command.Configuration);
            await unitOfWork.CompleteAsync();
            return Result<FarmDevice>.Success(device);
        }
        catch (Exception)
        {
            return Result<FarmDevice>.Failure(DeviceError.InvalidDeviceData);
        }
    }

    public async Task<Result> DeleteDeviceAsync(DeleteDeviceCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result.Failure(DeviceError.DeviceNotFound);

            deviceRepository.Remove(device);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(DeviceError.InvalidDeviceData);
        }
    }

    public async Task<Result> AssignDeviceAsync(AssignDeviceCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result.Failure(DeviceError.DeviceNotFound);

            var activeAssignment = await assignmentRepository.FindActiveByDeviceIdAsync(command.DeviceId);
            if (activeAssignment != null)
                return Result.Failure(DeviceError.DeviceAlreadyAssigned);

            device.AssignToAnimal(command.AnimalId);
            var assignment = new DeviceAssignment(command.DeviceId, command.AnimalId, command.UserId);
            await assignmentRepository.AddAsync(assignment);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(DeviceError.InvalidAssignmentData);
        }
    }

    public async Task<Result> UnassignDeviceAsync(UnassignDeviceCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result.Failure(DeviceError.DeviceNotFound);

            var assignment = await assignmentRepository.FindActiveByDeviceIdAsync(command.DeviceId);
            if (assignment == null)
                return Result.Failure(DeviceError.DeviceAssignmentNotFound);

            assignment.Unassign();
            device.UnassignFromAnimal();
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(DeviceError.InvalidAssignmentData);
        }
    }

    public async Task<Result<FarmDevice>> PingDeviceAsync(PingDeviceCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceNotFound);

            device.Ping();
            await unitOfWork.CompleteAsync();
            return Result<FarmDevice>.Success(device);
        }
        catch (Exception)
        {
            return Result<FarmDevice>.Failure(DeviceError.InvalidDeviceData);
        }
    }

    public async Task<Result<FarmDevice>> UpdateDeviceFirmwareAsync(UpdateDeviceFirmwareCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceNotFound);

            device.UpdateFirmware(command.Version);
            await unitOfWork.CompleteAsync();
            return Result<FarmDevice>.Success(device);
        }
        catch (Exception)
        {
            return Result<FarmDevice>.Failure(DeviceError.InvalidDeviceData);
        }
    }

    public async Task<Result<FarmDevice>> MarkDeviceMaintenanceAsync(MarkDeviceMaintenanceCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceNotFound);

            device.MarkAsMaintenance();
            await unitOfWork.CompleteAsync();
            return Result<FarmDevice>.Success(device);
        }
        catch (Exception)
        {
            return Result<FarmDevice>.Failure(DeviceError.InvalidDeviceData);
        }
    }

    public async Task<Result<FarmDevice>> UpdateDeviceConfigurationAsync(UpdateDeviceConfigurationCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceNotFound);

            device.UpdateReadingInterval(command.Configuration);
            await unitOfWork.CompleteAsync();
            return Result<FarmDevice>.Success(device);
        }
        catch (Exception)
        {
            return Result<FarmDevice>.Failure(DeviceError.InvalidDeviceData);
        }
    }

    public async Task<Result> BatchAssignDevicesAsync(BatchAssignDevicesCommand command)
    {
        try
        {
            foreach (var item in command.Assignments)
            {
                var device = await deviceRepository.FindByIdAsync(item.DeviceId);
                if (device == null)
                    return Result.Failure(DeviceError.DeviceNotFound);

                device.AssignToAnimal(item.AnimalId);
                var assignment = new DeviceAssignment(item.DeviceId, item.AnimalId, command.UserId);
                await assignmentRepository.AddAsync(assignment);
            }

            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(DeviceError.InvalidAssignmentData);
        }
    }

    public async Task<Result<FarmDevice>> UpdateReadingIntervalAsync(UpdateReadingIntervalCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceNotFound);

            device.UpdateReadingInterval(command.Configuration);
            await unitOfWork.CompleteAsync();
            return Result<FarmDevice>.Success(device);
        }
        catch (Exception)
        {
            return Result<FarmDevice>.Failure(DeviceError.InvalidDeviceData);
        }
    }

    public async Task<Result<FarmDevice>> UpdateAlertThresholdsAsync(UpdateAlertThresholdsCommand command)
    {
        try
        {
            var device = await deviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null)
                return Result<FarmDevice>.Failure(DeviceError.DeviceNotFound);

            device.UpdateAlertThresholds(command.Configuration);
            await unitOfWork.CompleteAsync();
            return Result<FarmDevice>.Success(device);
        }
        catch (Exception)
        {
            return Result<FarmDevice>.Failure(DeviceError.InvalidDeviceData);
        }
    }
}
