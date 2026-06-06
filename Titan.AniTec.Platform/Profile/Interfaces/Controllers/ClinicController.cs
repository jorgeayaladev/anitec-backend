using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Profile.Application.CommandServices;
using Titan.AniTec.Platform.Profile.Domain.Repositories;
using Titan.AniTec.Platform.Profile.Interfaces.Assemblers;
using Titan.AniTec.Platform.Profile.Interfaces.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Profile.Interfaces.Controllers;

[ApiController]
[Route("api/profile")]
[Authorize]
public class ClinicController(
    IProfileQueryService queryService,
    IProfileCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    [HttpGet("clinic")]
    public async Task<IActionResult> GetClinic()
    {
        var query = new GetClinicQuery(CurrentUserId);
        var result = await queryService.GetClinicAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpPut("clinic")]
    public async Task<IActionResult> UpdateClinic([FromBody] UpdateClinicResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.UpdateClinicAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpGet("clinic/schedule")]
    public async Task<IActionResult> GetSchedule()
    {
        var query = new GetClinicScheduleQuery(CurrentUserId);
        var result = await queryService.GetClinicScheduleAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(list => list.Select(ProfileAssembler.ToResource).ToList()));
    }

    [HttpPut("clinic/schedule")]
    public async Task<IActionResult> UpdateSchedule([FromBody] UpdateClinicScheduleResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.UpdateClinicScheduleAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("clinic/specialties")]
    public async Task<IActionResult> GetSpecialties()
    {
        var query = new GetSpecialtiesQuery(CurrentUserId);
        var result = await queryService.GetSpecialtiesAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(list => list.Select(ProfileAssembler.ToResource).ToList()));
    }

    [HttpPost("clinic/specialties")]
    public async Task<IActionResult> AddSpecialty([FromBody] CreateSpecialtyResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.AddSpecialtyAsync(command);
        return ProfileActionResultAssembler.ToCreatedActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpDelete("clinic/specialties/{specialtyId:int}")]
    public async Task<IActionResult> RemoveSpecialty(int specialtyId)
    {
        var command = new RemoveSpecialtyCommand(CurrentUserId, specialtyId);
        var result = await commandService.RemoveSpecialtyAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("clinic/license")]
    public async Task<IActionResult> GetLicense()
    {
        var query = new GetProfessionalLicenseQuery(CurrentUserId);
        var result = await queryService.GetProfessionalLicenseAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpPut("clinic/license")]
    public async Task<IActionResult> UpdateLicense([FromBody] UpdateProfessionalLicenseResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.UpdateProfessionalLicenseAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpGet("clinic/service-area")]
    public async Task<IActionResult> GetServiceArea()
    {
        var query = new GetServiceAreaQuery(CurrentUserId);
        var result = await queryService.GetServiceAreaAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(list => list.Select(ProfileAssembler.ToResource).ToList()));
    }

    [HttpPost("clinic/service-area/locations")]
    public async Task<IActionResult> AddServiceAreaLocation([FromBody] CreateServiceAreaLocationResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.AddServiceAreaLocationAsync(command);
        return ProfileActionResultAssembler.ToCreatedActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpDelete("clinic/service-area/locations/{locationId:int}")]
    public async Task<IActionResult> RemoveServiceAreaLocation(int locationId)
    {
        var command = new RemoveServiceAreaLocationCommand(CurrentUserId, locationId);
        var result = await commandService.RemoveServiceAreaLocationAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("clinic/availability")]
    public async Task<IActionResult> GetAvailability()
    {
        var query = new GetAvailabilityQuery(CurrentUserId);
        var result = await queryService.GetAvailabilityAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(list => list.Select(ProfileAssembler.ToResource).ToList()));
    }

    [HttpPut("clinic/availability")]
    public async Task<IActionResult> UpdateAvailability([FromBody] UpdateAvailabilityResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.UpdateAvailabilityAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("clinic/service-area")]
    public async Task<IActionResult> UpdateServiceArea([FromBody] UpdateServiceAreaResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.UpdateServiceAreaAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }
}
