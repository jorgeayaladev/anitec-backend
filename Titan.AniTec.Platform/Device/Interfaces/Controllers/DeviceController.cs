using Titan.AniTec.Platform.Device.Application.CommandServices;
using Titan.AniTec.Platform.Device.Domain.Repositories;
using Titan.AniTec.Platform.Device.Interfaces.Assemblers;
using Titan.AniTec.Platform.Device.Interfaces.Resources;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Device.Interfaces.Controllers;

[ApiController]
[Route("api/devices")]
[Authorize]
public class DeviceController(
    IDeviceQueryService queryService,
    IDeviceCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    // --- Tipos de Dispositivos ---
    [HttpGet("types")]
    public async Task<IActionResult> GetAllDeviceTypes()
    {
        var query = new GetAllDeviceTypesQuery();
        var result = await queryService.GetAllDeviceTypesAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    [HttpGet("types/{typeId:int}")]
    public async Task<IActionResult> GetDeviceTypeById(int typeId)
    {
        var query = new GetDeviceTypeByIdQuery(typeId);
        var result = await queryService.GetDeviceTypeByIdAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpGet("types/by-category/{category}")]
    public async Task<IActionResult> GetDeviceTypesByCategory(string category)
    {
        var query = new GetDeviceTypesByCategoryQuery(category);
        var result = await queryService.GetDeviceTypesByCategoryAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    [HttpPost("types")]
    public async Task<IActionResult> CreateDeviceType([FromBody] CreateDeviceTypeResource resource)
    {
        var command = DeviceAssembler.ToCommand(resource);
        var result = await commandService.RegisterDeviceTypeAsync(command);
        return DeviceActionResultAssembler.ToCreatedActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpPut("types/{typeId:int}")]
    public async Task<IActionResult> UpdateDeviceType(int typeId, [FromBody] UpdateDeviceTypeResource resource)
    {
        var command = DeviceAssembler.ToCommand(typeId, resource);
        var result = await commandService.UpdateDeviceTypeAsync(command);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpDelete("types/{typeId:int}")]
    public async Task<IActionResult> DeleteDeviceType(int typeId)
    {
        var command = new DeleteDeviceTypeCommand(typeId);
        var result = await commandService.DeleteDeviceTypeAsync(command);
        return DeviceActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("types/{typeId:int}/specifications")]
    public async Task<IActionResult> GetDeviceTypeSpecifications(int typeId)
    {
        var query = new GetDeviceTypeByIdQuery(typeId);
        var result = await queryService.GetDeviceTypeByIdAsync(query);
        if (result.IsSuccess)
            return Ok(new { Specifications = result.Value?.Specifications });
        return DeviceActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("types/{typeId:int}/metrics")]
    public async Task<IActionResult> GetDeviceTypeMetrics(int typeId)
    {
        var query = new GetDeviceTypeByIdQuery(typeId);
        var result = await queryService.GetDeviceTypeByIdAsync(query);
        if (result.IsSuccess)
            return Ok(new { Metrics = result.Value?.Metrics });
        return DeviceActionResultAssembler.ToActionResult(result);
    }

    // --- Dispositivos ---
    [HttpGet]
    public async Task<IActionResult> GetAllDevices()
    {
        var query = new GetAllDevicesQuery(CurrentUserId);
        var result = await queryService.GetAllDevicesAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    [HttpGet("{deviceId:int}")]
    public async Task<IActionResult> GetDeviceById(int deviceId)
    {
        var query = new GetDeviceByIdQuery(CurrentUserId, deviceId);
        var result = await queryService.GetDeviceByIdAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpPost]
    public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceResource resource)
    {
        var command = DeviceAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.RegisterDeviceAsync(command);
        return DeviceActionResultAssembler.ToCreatedActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpPut("{deviceId:int}")]
    public async Task<IActionResult> UpdateDevice(int deviceId, [FromBody] UpdateDeviceResource resource)
    {
        var command = DeviceAssembler.ToCommand(CurrentUserId, deviceId, resource);
        var result = await commandService.UpdateDeviceAsync(command);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpDelete("{deviceId:int}")]
    public async Task<IActionResult> DeleteDevice(int deviceId)
    {
        var command = new DeleteDeviceCommand(CurrentUserId, deviceId);
        var result = await commandService.DeleteDeviceAsync(command);
        return DeviceActionResultAssembler.ToActionResult(result);
    }

    // --- Filtros y Búsqueda ---
    [HttpGet("by-type/{type}")]
    public async Task<IActionResult> GetDevicesByType(string type)
    {
        if (int.TryParse(type, out var typeId))
        {
            var query = new GetDevicesByTypeQuery(CurrentUserId, typeId);
            var result = await queryService.GetDevicesByTypeAsync(query);
            return DeviceActionResultAssembler.ToActionResult(
                result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
        }
        return Ok(new List<object>());
    }

    [HttpGet("by-status/{status}")]
    public async Task<IActionResult> GetDevicesByStatus(string status)
    {
        var query = new GetDevicesByStatusQuery(CurrentUserId, status);
        var result = await queryService.GetDevicesByStatusAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    [HttpGet("unassigned")]
    public async Task<IActionResult> GetUnassignedDevices()
    {
        var query = new GetUnassignedDevicesQuery(CurrentUserId);
        var result = await queryService.GetUnassignedDevicesAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchDevices([FromQuery] string q)
    {
        var query = new SearchDevicesQuery(CurrentUserId, q);
        var result = await queryService.SearchDevicesAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    // --- Asignación ---
    [HttpPost("{deviceId:int}/assign/{animalId:int}")]
    public async Task<IActionResult> AssignDevice(int deviceId, int animalId)
    {
        var command = new AssignDeviceCommand(CurrentUserId, deviceId, animalId);
        var result = await commandService.AssignDeviceAsync(command);
        return DeviceActionResultAssembler.ToActionResult(result);
    }

    [HttpPost("{deviceId:int}/unassign")]
    public async Task<IActionResult> UnassignDevice(int deviceId)
    {
        var command = new UnassignDeviceCommand(CurrentUserId, deviceId);
        var result = await commandService.UnassignDeviceAsync(command);
        return DeviceActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("{deviceId:int}/assignment-history")]
    public async Task<IActionResult> GetAssignmentHistory(int deviceId)
    {
        var query = new GetDeviceAssignmentHistoryQuery(CurrentUserId, deviceId);
        var result = await queryService.GetDeviceAssignmentHistoryAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    [HttpPost("assign/batch")]
    public async Task<IActionResult> BatchAssignDevices([FromBody] BatchAssignDevicesResource resource)
    {
        var command = DeviceAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.BatchAssignDevicesAsync(command);
        return DeviceActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("animals/{animalId:int}/devices")]
    public async Task<IActionResult> GetDevicesByAnimal(int animalId)
    {
        var query = new GetDevicesByAnimalQuery(CurrentUserId, animalId);
        var result = await queryService.GetDevicesByAnimalAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    [HttpGet("by-animal/{animalId:int}")]
    public async Task<IActionResult> GetDevicesByAnimalAlt(int animalId)
    {
        var query = new GetDevicesByAnimalQuery(CurrentUserId, animalId);
        var result = await queryService.GetDevicesByAnimalAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    [HttpGet("by-location/{locationId:int}")]
    public async Task<IActionResult> GetDevicesByLocation(int locationId)
    {
        var query = new GetDevicesByLocationQuery(CurrentUserId, locationId);
        var result = await queryService.GetDevicesByLocationAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }

    // --- Monitoreo ---
    [HttpPost("{deviceId:int}/ping")]
    public async Task<IActionResult> PingDevice(int deviceId)
    {
        var command = new PingDeviceCommand(CurrentUserId, deviceId);
        var result = await commandService.PingDeviceAsync(command);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpGet("{deviceId:int}/status")]
    public async Task<IActionResult> GetDeviceStatus(int deviceId)
    {
        var query = new GetDeviceStatusQuery(CurrentUserId, deviceId);
        var result = await queryService.GetDeviceStatusAsync(query);
        return DeviceActionResultAssembler.ToActionResult(result.Map(s => new { Status = s }));
    }

    [HttpGet("{deviceId:int}/battery")]
    public async Task<IActionResult> GetDeviceBattery(int deviceId)
    {
        var query = new GetDeviceBatteryQuery(CurrentUserId, deviceId);
        var result = await queryService.GetDeviceBatteryAsync(query);
        return DeviceActionResultAssembler.ToActionResult(result.Map(b => new { BatteryLevel = b }));
    }

    [HttpGet("{deviceId:int}/signal")]
    public async Task<IActionResult> GetDeviceSignal(int deviceId)
    {
        var query = new GetDeviceSignalQuery(CurrentUserId, deviceId);
        var result = await queryService.GetDeviceSignalAsync(query);
        return DeviceActionResultAssembler.ToActionResult(result.Map(s => new { SignalStrength = s }));
    }

    [HttpGet("{deviceId:int}/last-reading")]
    public async Task<IActionResult> GetDeviceLastReading(int deviceId)
    {
        var query = new GetDeviceLastReadingQuery(CurrentUserId, deviceId);
        var result = await queryService.GetDeviceLastReadingAsync(query);
        return DeviceActionResultAssembler.ToActionResult(result.Map(r => new { LastReadingAt = r }));
    }

    [HttpPut("{deviceId:int}/firmware")]
    public async Task<IActionResult> UpdateFirmware(int deviceId, [FromBody] UpdateDeviceFirmwareResource resource)
    {
        var command = new UpdateDeviceFirmwareCommand(CurrentUserId, deviceId, resource.Version);
        var result = await commandService.UpdateDeviceFirmwareAsync(command);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpPut("{deviceId:int}/maintenance")]
    public async Task<IActionResult> MarkMaintenance(int deviceId)
    {
        var command = new MarkDeviceMaintenanceCommand(CurrentUserId, deviceId);
        var result = await commandService.MarkDeviceMaintenanceAsync(command);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    // --- Configuración ---
    [HttpGet("{deviceId:int}/configuration")]
    public async Task<IActionResult> GetConfiguration(int deviceId)
    {
        var query = new GetDeviceByIdQuery(CurrentUserId, deviceId);
        var result = await queryService.GetDeviceByIdAsync(query);
        if (result.IsSuccess)
            return Ok(new { Configuration = result.Value?.Configuration });
        return DeviceActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("{deviceId:int}/configuration")]
    public async Task<IActionResult> UpdateConfiguration(int deviceId, [FromBody] UpdateDeviceConfigurationResource resource)
    {
        var command = new UpdateDeviceConfigurationCommand(CurrentUserId, deviceId, resource.Configuration);
        var result = await commandService.UpdateDeviceConfigurationAsync(command);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpPut("{deviceId:int}/reading-interval")]
    public async Task<IActionResult> UpdateReadingInterval(int deviceId, [FromBody] UpdateReadingIntervalResource resource)
    {
        var command = new UpdateReadingIntervalCommand(CurrentUserId, deviceId, resource.Configuration);
        var result = await commandService.UpdateReadingIntervalAsync(command);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    [HttpPut("{deviceId:int}/alert-thresholds")]
    public async Task<IActionResult> UpdateAlertThresholds(int deviceId, [FromBody] UpdateAlertThresholdsResource resource)
    {
        var command = new UpdateAlertThresholdsCommand(CurrentUserId, deviceId, resource.Configuration);
        var result = await commandService.UpdateAlertThresholdsAsync(command);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(DeviceAssembler.ToResource));
    }

    // --- Firmware ---
    [HttpGet("{deviceId:int}/firmware/history")]
    public async Task<IActionResult> GetFirmwareHistory(int deviceId)
    {
        return Ok(new List<object>());
    }

    // --- Config Templates ---
    [HttpGet("{deviceId:int}/configuration/templates")]
    public async Task<IActionResult> GetConfigTemplates(int deviceId)
    {
        return Ok(new List<object>());
    }

    [HttpPost("{deviceId:int}/configuration/templates")]
    public async Task<IActionResult> CreateConfigTemplate(int deviceId, [FromBody] CreateConfigTemplateResource resource)
    {
        return CreatedAtAction(nameof(GetConfigTemplates), new { deviceId, id = 0 },
            new ConfigTemplateResource(0, resource.Name, resource.Description, resource.Configuration));
    }

    // --- Alertas ---
    [HttpGet("alerts")]
    public async Task<IActionResult> GetDeviceAlerts()
    {
        var query = new GetDeviceAlertsQuery(CurrentUserId);
        var result = await queryService.GetDeviceAlertsAsync(query);
        return DeviceActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(DeviceAssembler.ToResource).ToList()));
    }
}
