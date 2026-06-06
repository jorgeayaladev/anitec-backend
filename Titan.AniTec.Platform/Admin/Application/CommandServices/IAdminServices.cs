using Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Admin.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Admin.Application.CommandServices;

public interface IAdminCommandService
{
    Task<Result> UpdateSystemSettingAsync(UpdateSystemSettingCommand command);
    Task<Result> UpdateEmailSettingsAsync(UpdateEmailSettingsCommand command);
    Task<Result> UpdateNotificationSystemSettingsAsync(UpdateNotificationSettingsCommand command);
    Task<Result> ConfigureStripeAsync(ConfigureStripeCommand command);
    Task<Result> ConfigureIotAsync(ConfigureIotCommand command);
    Task<Result> UpdateContentPageAsync(UpdateContentPageCommand command);
    Task<Result<Faq>> CreateFaqAsync(CreateFaqCommand command);
    Task<Result<Faq>> UpdateFaqAsync(UpdateFaqCommand command);
    Task<Result> DeleteFaqAsync(DeleteFaqCommand command);
    Task<Result<Announcement>> CreateAnnouncementAsync(CreateAnnouncementCommand command);
    Task<Result<Announcement>> UpdateAnnouncementAsync(UpdateAnnouncementCommand command);
    Task<Result> DeleteAnnouncementAsync(DeleteAnnouncementCommand command);
}

public interface IAdminQueryService
{
    Task<Result<object>> GetAdminDashboardAsync(GetAdminDashboardQuery query);
    Task<Result<object>> GetAdminStatisticsAsync(GetAdminStatisticsQuery query);
    Task<Result<object>> GetAuditLogsAsync(GetAuditLogsQuery query);
    Task<Result<object>> GetAuditLogByIdAsync(GetAuditLogByIdQuery query);
    Task<Result<object>> GetAuditLogsByUserAsync(GetAuditLogsByUserQuery query);
    Task<Result<object>> GetAuditLogsByEntityAsync(GetAuditLogsByEntityQuery query);
    Task<Result<object>> GetSystemSettingsAsync(GetSystemSettingsQuery query);
    Task<Result<object>> GetEmailSettingsAsync(GetEmailSettingsQuery query);
    Task<Result<object>> GetNotificationSystemSettingsAsync(GetNotificationSystemSettingsQuery query);
    Task<Result<object>> GetIntegrationSettingsAsync(GetIntegrationSettingsQuery query);
    Task<Result<IReadOnlyCollection<ContentPage>>> GetContentPagesAsync(GetContentPagesQuery query);
    Task<Result<IReadOnlyCollection<Faq>>> GetFaqsAsync(GetFaqsQuery query);
    Task<Result<IReadOnlyCollection<Announcement>>> GetAnnouncementsAsync(GetAnnouncementsQuery query);
}
