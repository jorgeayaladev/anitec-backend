using Titan.AniTec.Platform.Admin.Application.CommandServices;
using Titan.AniTec.Platform.Admin.Domain.Repositories;
using Titan.AniTec.Platform.Admin.Interfaces.Assemblers;
using Titan.AniTec.Platform.Admin.Interfaces.Resources;
using Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Admin.Interfaces.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize]
public class AdminController(
    IAdminQueryService queryService,
    IAdminCommandService commandService) : ControllerBase
{
    private int CurrentUserId => ((User)HttpContext.Items["User"]!).Id;

    // Dashboard
    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var result = await queryService.GetAdminDashboardAsync(new GetAdminDashboardQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var result = await queryService.GetAdminStatisticsAsync(new GetAdminStatisticsQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    // Audit Logs
    [HttpGet("audit-logs")]
    public async Task<IActionResult> GetAuditLogs()
    {
        var result = await queryService.GetAuditLogsAsync(new GetAuditLogsQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("audit-logs/{logId:int}")]
    public async Task<IActionResult> GetAuditLogById(int logId)
    {
        var result = await queryService.GetAuditLogByIdAsync(new GetAuditLogByIdQuery(CurrentUserId, logId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("audit-logs/by-user/{targetUserId:int}")]
    public async Task<IActionResult> GetAuditLogsByUser(int targetUserId)
    {
        var result = await queryService.GetAuditLogsByUserAsync(new GetAuditLogsByUserQuery(CurrentUserId, targetUserId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("audit-logs/by-entity/{entityType}/{entityId:int}")]
    public async Task<IActionResult> GetAuditLogsByEntity(string entityType, int entityId)
    {
        var result = await queryService.GetAuditLogsByEntityAsync(new GetAuditLogsByEntityQuery(CurrentUserId, entityType, entityId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    // System Settings
    [HttpGet("settings")]
    public async Task<IActionResult> GetSystemSettings()
    {
        var result = await queryService.GetSystemSettingsAsync(new GetSystemSettingsQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("settings")]
    public async Task<IActionResult> UpdateSystemSetting([FromBody] UpdateSystemSettingResource resource)
    {
        var command = new UpdateSystemSettingCommand(CurrentUserId, resource.Key, resource.Value);
        var result = await commandService.UpdateSystemSettingAsync(command);
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("settings/email")]
    public async Task<IActionResult> GetEmailSettings()
    {
        var result = await queryService.GetEmailSettingsAsync(new GetEmailSettingsQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("settings/email")]
    public async Task<IActionResult> UpdateEmailSettings([FromBody] EmailSettingsResource resource)
    {
        var command = new UpdateEmailSettingsCommand(CurrentUserId,
            resource.SmtpHost, resource.SmtpPort, resource.Username, resource.Password,
            resource.FromAddress, resource.FromName);
        var result = await commandService.UpdateEmailSettingsAsync(command);
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("settings/notifications")]
    public async Task<IActionResult> GetNotificationSettings()
    {
        var result = await queryService.GetNotificationSystemSettingsAsync(new GetNotificationSystemSettingsQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("settings/notifications")]
    public async Task<IActionResult> UpdateNotificationSettings([FromBody] NotificationSystemSettingsResource resource)
    {
        var command = new UpdateNotificationSettingsCommand(CurrentUserId,
            resource.EmailEnabled, resource.PushEnabled, resource.SmsEnabled);
        var result = await commandService.UpdateNotificationSystemSettingsAsync(command);
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("settings/integrations")]
    public async Task<IActionResult> GetIntegrationSettings()
    {
        var result = await queryService.GetIntegrationSettingsAsync(new GetIntegrationSettingsQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("settings/integrations/stripe")]
    public async Task<IActionResult> ConfigureStripe([FromBody] StripeConfigResource resource)
    {
        var command = new ConfigureStripeCommand(CurrentUserId,
            resource.SecretKey, resource.PublishableKey, resource.WebhookSecret);
        var result = await commandService.ConfigureStripeAsync(command);
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpPut("settings/integrations/iot")]
    public async Task<IActionResult> ConfigureIot([FromBody] IotConfigResource resource)
    {
        var command = new ConfigureIotCommand(CurrentUserId,
            resource.MqttBrokerUrl, resource.MqttPort, resource.MqttUsername, resource.MqttPassword);
        var result = await commandService.ConfigureIotAsync(command);
        return AdminActionResultAssembler.ToActionResult(result);
    }

    // Content Management
    [HttpGet("content/pages")]
    public async Task<IActionResult> GetContentPages()
    {
        var result = await queryService.GetContentPagesAsync(new GetContentPagesQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpPut("content/pages/{pageId:int}")]
    public async Task<IActionResult> UpdateContentPage(int pageId, [FromBody] UpdateContentPageResource resource)
    {
        var command = new UpdateContentPageCommand(CurrentUserId, pageId, resource.Title, resource.Content);
        var result = await commandService.UpdateContentPageAsync(command);
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("content/faqs")]
    public async Task<IActionResult> GetFaqs()
    {
        var result = await queryService.GetFaqsAsync(new GetFaqsQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpPost("content/faqs")]
    public async Task<IActionResult> CreateFaq([FromBody] CreateFaqResource resource)
    {
        var command = AdminAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.CreateFaqAsync(command);
        return AdminActionResultAssembler.ToCreatedActionResult(result.Map(e => e.ToResource()));
    }

    [HttpPut("content/faqs/{faqId:int}")]
    public async Task<IActionResult> UpdateFaq(int faqId, [FromBody] UpdateFaqResource resource)
    {
        var command = AdminAssembler.ToCommand(CurrentUserId, faqId, resource);
        var result = await commandService.UpdateFaqAsync(command);
        return AdminActionResultAssembler.ToActionResult(result.Map(e => e.ToResource()));
    }

    [HttpDelete("content/faqs/{faqId:int}")]
    public async Task<IActionResult> DeleteFaq(int faqId)
    {
        var command = new DeleteFaqCommand(CurrentUserId, faqId);
        var result = await commandService.DeleteFaqAsync(command);
        return AdminActionResultAssembler.ToActionResult(result);
    }

    [HttpGet("content/announcements")]
    public async Task<IActionResult> GetAnnouncements()
    {
        var result = await queryService.GetAnnouncementsAsync(new GetAnnouncementsQuery(CurrentUserId));
        return AdminActionResultAssembler.ToActionResult(
            result.Map(list => list.Select(e => e.ToResource()).ToList()));
    }

    [HttpPost("content/announcements")]
    public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementResource resource)
    {
        var command = AdminAssembler.ToCommand(CurrentUserId, resource);
        var result = await commandService.CreateAnnouncementAsync(command);
        return AdminActionResultAssembler.ToCreatedActionResult(result.Map(e => e.ToResource()));
    }

    [HttpPut("content/announcements/{announcementId:int}")]
    public async Task<IActionResult> UpdateAnnouncement(int announcementId, [FromBody] UpdateAnnouncementResource resource)
    {
        var command = AdminAssembler.ToCommand(CurrentUserId, announcementId, resource);
        var result = await commandService.UpdateAnnouncementAsync(command);
        return AdminActionResultAssembler.ToActionResult(result.Map(e => e.ToResource()));
    }

    [HttpDelete("content/announcements/{announcementId:int}")]
    public async Task<IActionResult> DeleteAnnouncement(int announcementId)
    {
        var command = new DeleteAnnouncementCommand(CurrentUserId, announcementId);
        var result = await commandService.DeleteAnnouncementAsync(command);
        return AdminActionResultAssembler.ToActionResult(result);
    }
}
