using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Device.Domain.Model;
using Titan.AniTec.Platform.Device.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Device.Domain.Repositories;
using Titan.AniTec.Platform.Device.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Device.Interfaces.Assemblers;

public static class DeviceAssembler
{
    // --- DeviceType ---
    public static DeviceTypeResource ToResource(DeviceType deviceType)
        => new(deviceType.Id, deviceType.Name, deviceType.Description, deviceType.Category,
            deviceType.Specifications, deviceType.Metrics);

    public static RegisterDeviceTypeCommand ToCommand(CreateDeviceTypeResource resource)
        => new(resource.Name, resource.Description, resource.Category,
            resource.Specifications, resource.Metrics);

    public static UpdateDeviceTypeCommand ToCommand(int deviceTypeId, UpdateDeviceTypeResource resource)
        => new(deviceTypeId, resource.Name, resource.Description, resource.Category,
            resource.Specifications, resource.Metrics);

    // --- Device ---
    public static DeviceResource ToResource(FarmDevice device)
        => new(device.Id, device.FarmId, device.DeviceTypeId, device.Name, device.SerialNumber,
            device.Status, device.FirmwareVersion, device.LastPingAt, device.BatteryLevel,
            device.SignalStrength, device.LastReadingAt, device.CurrentAnimalId,
            device.CurrentLocationId, device.Configuration);

    public static RegisterDeviceCommand ToCommand(int userId, CreateDeviceResource resource)
        => new(userId, resource.DeviceTypeId, resource.Name, resource.SerialNumber,
            resource.FirmwareVersion, resource.CurrentLocationId, resource.Configuration);

    public static UpdateDeviceCommand ToCommand(int userId, int deviceId, UpdateDeviceResource resource)
        => new(userId, deviceId, resource.Name, resource.SerialNumber, resource.DeviceTypeId,
            resource.FirmwareVersion, resource.CurrentLocationId, resource.Configuration);

    // --- Alert ---
    public static DeviceAlertResource ToResource(DeviceAlert alert)
        => new(alert.Id, alert.DeviceId, alert.FarmId, alert.AlertType, alert.Description,
            alert.IsResolved, alert.ResolvedAt);

    // --- Assignment ---
    public static DeviceAssignmentResource ToResource(DeviceAssignment assignment)
        => new(assignment.Id, assignment.DeviceId, assignment.AnimalId, assignment.FarmId,
            assignment.AssignedAt, assignment.UnassignedAt, assignment.IsActive);

    // --- Batch ---
    public static BatchAssignDevicesCommand ToCommand(int userId, BatchAssignDevicesResource resource)
        => new(userId, resource.Assignments.Select(a => new BatchAssignItem(a.DeviceId, a.AnimalId)).ToList());
}

public static class DeviceActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsFailure)
        {
            return result.Error switch
            {
                DeviceError.DeviceNotFound or
                DeviceError.DeviceTypeNotFound or
                DeviceError.DeviceAssignmentNotFound or
                DeviceError.DeviceAlertNotFound => new NotFoundObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                DeviceError.DeviceAlreadyExists or
                DeviceError.DeviceTypeAlreadyExists or
                DeviceError.DeviceAlreadyAssigned => new ConflictObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                _ => new BadRequestObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() })
            };
        }
        return new OkObjectResult(result.Value);
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsFailure)
            return ToActionResult(result);
        return new CreatedResult(string.Empty, result.Value);
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsFailure)
        {
            return result.Error switch
            {
                DeviceError.DeviceNotFound or
                DeviceError.DeviceTypeNotFound or
                DeviceError.DeviceAssignmentNotFound => new NotFoundObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() }),
                _ => new BadRequestObjectResult(new
                    { Message = result.Message, Error = result.Error!.ToString() })
            };
        }
        return new OkResult();
    }
}
