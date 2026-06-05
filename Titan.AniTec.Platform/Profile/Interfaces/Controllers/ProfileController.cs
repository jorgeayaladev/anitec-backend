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
public class ProfileController(
    IProfileQueryService queryService,
    IProfileCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var query = new GetMyProfileQuery(CurrentUserId);
        var result = await queryService.GetMyProfileAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.UpdateProfileAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpGet("me/preferences")]
    public async Task<IActionResult> GetPreferences()
    {
        var query = new GetMyPreferencesQuery(CurrentUserId);
        var result = await queryService.GetMyPreferencesAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpPut("me/preferences")]
    public async Task<IActionResult> UpdatePreferences([FromBody] UpdatePreferencesResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.UpdatePreferencesAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpPut("me/notifications")]
    public async Task<IActionResult> UpdateNotificationSettings([FromBody] UpdateNotificationSettingsResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.UpdateNotificationSettingsAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpGet("me/notification-history")]
    public async Task<IActionResult> GetNotificationHistory()
    {
        var query = new GetNotificationHistoryQuery(CurrentUserId);
        var result = await queryService.GetNotificationHistoryAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(list => list.Select(ProfileAssembler.ToResource).ToList()));
    }

    [HttpPut("me/notification/{notificationId:int}/read")]
    public async Task<IActionResult> MarkNotificationRead(int notificationId)
    {
        var command = new MarkNotificationReadCommand(CurrentUserId, notificationId);
        var result = await commandService.MarkNotificationReadAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("me/notifications/read-all")]
    public async Task<IActionResult> MarkAllNotificationsRead()
    {
        var command = new MarkAllNotificationsReadCommand(CurrentUserId);
        var result = await commandService.MarkAllNotificationsReadAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farm")]
    public async Task<IActionResult> GetFarm()
    {
        var query = new GetFarmQuery(CurrentUserId);
        var result = await queryService.GetFarmAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpPut("farm")]
    public async Task<IActionResult> UpdateFarm([FromBody] UpdateFarmResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.UpdateFarmAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpGet("farm/locations")]
    public async Task<IActionResult> GetFarmLocations()
    {
        var query = new GetFarmLocationsQuery(CurrentUserId);
        var result = await queryService.GetFarmLocationsAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(list => list.Select(ProfileAssembler.ToResource).ToList()));
    }

    [HttpPost("farm/locations")]
    public async Task<IActionResult> AddFarmLocation([FromBody] CreateFarmLocationResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.AddFarmLocationAsync(command);
        return ProfileActionResultAssembler.ToCreatedActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpPut("farm/locations/{locationId:int}")]
    public async Task<IActionResult> UpdateFarmLocation(int locationId, [FromBody] UpdateFarmLocationResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, locationId, resource);
        var result = await commandService.UpdateFarmLocationAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpDelete("farm/locations/{locationId:int}")]
    public async Task<IActionResult> RemoveFarmLocation(int locationId)
    {
        var command = new RemoveFarmLocationCommand(CurrentUserId, locationId);
        var result = await commandService.RemoveFarmLocationAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farm/staff")]
    public async Task<IActionResult> GetFarmStaff()
    {
        var query = new GetFarmStaffQuery(CurrentUserId);
        var result = await queryService.GetFarmStaffAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(list => list.Select(ProfileAssembler.ToResource).ToList()));
    }

    [HttpPost("farm/staff")]
    public async Task<IActionResult> AddFarmStaff([FromBody] CreateFarmStaffResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.AddFarmStaffAsync(command);
        return ProfileActionResultAssembler.ToCreatedActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpPut("farm/staff/{staffId:int}")]
    public async Task<IActionResult> UpdateFarmStaff(int staffId, [FromBody] UpdateFarmStaffResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, staffId, resource);
        var result = await commandService.UpdateFarmStaffAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpDelete("farm/staff/{staffId:int}")]
    public async Task<IActionResult> RemoveFarmStaff(int staffId)
    {
        var command = new RemoveFarmStaffCommand(CurrentUserId, staffId);
        var result = await commandService.RemoveFarmStaffAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("farm/certifications")]
    public async Task<IActionResult> GetFarmCertifications()
    {
        var query = new GetFarmCertificationsQuery(CurrentUserId);
        var result = await queryService.GetFarmCertificationsAsync(query);
        return ProfileActionResultAssembler.ToActionResult(result.Map(list => list.Select(ProfileAssembler.ToResource).ToList()));
    }

    [HttpPost("farm/certifications")]
    public async Task<IActionResult> AddFarmCertification([FromBody] CreateFarmCertificationResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.AddFarmCertificationAsync(command);
        return ProfileActionResultAssembler.ToCreatedActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpPut("farm/certifications/{certificationId:int}")]
    public async Task<IActionResult> UpdateFarmCertification(int certificationId, [FromBody] UpdateFarmCertificationResource resource)
    {
        var command = ProfileAssembler.ToCommand(CurrentUserId, certificationId, resource);
        var result = await commandService.UpdateFarmCertificationAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result.Map(ProfileAssembler.ToResource));
    }

    [HttpDelete("farm/certifications/{certificationId:int}")]
    public async Task<IActionResult> RemoveFarmCertification(int certificationId)
    {
        var command = new RemoveFarmCertificationCommand(CurrentUserId, certificationId);
        var result = await commandService.RemoveFarmCertificationAsync(command);
        return ProfileActionResultAssembler.ToActionResult(result);
    }
}
