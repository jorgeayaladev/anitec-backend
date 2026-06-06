using Titan.AniTec.Platform.Admin.Domain.Model;
using Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Admin.Domain.Repositories;
using Titan.AniTec.Platform.Admin.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Admin.Application.Internal.QueryServices;

public class AdminQueryService(
    ISystemSettingRepository systemSettingRepository,
    IAuditLogRepository auditLogRepository,
    IContentPageRepository contentPageRepository,
    IFaqRepository faqRepository,
    IAnnouncementRepository announcementRepository) : IAdminQueryService
{
    public Task<Result<object>> GetAdminDashboardAsync(GetAdminDashboardQuery query) =>
        Task.FromResult(Result<object>.Success(new
        {
            TotalUsers = 0, ActiveUsers = 0, TotalFarms = 0,
            TotalSubscriptions = 0, ActiveSubscriptions = 0,
            RevenueThisMonth = 0m, TotalAnimals = 0
        }));

    public Task<Result<object>> GetAdminStatisticsAsync(GetAdminStatisticsQuery query) =>
        Task.FromResult(Result<object>.Success(new
        {
            UsersByRole = new { }, SubscriptionsByPlan = new { },
            StorageUsed = "0 GB", ApiCallsToday = 0
        }));

    public async Task<Result<object>> GetAuditLogsAsync(GetAuditLogsQuery query)
    {
        try
        {
            var logs = await auditLogRepository.GetAllAsync();
            return Result<object>.Success(new { Logs = logs, Total = logs.Count });
        }
        catch { return Result<object>.Failure(AdminError.AuditLogNotFound); }
    }

    public async Task<Result<object>> GetAuditLogByIdAsync(GetAuditLogByIdQuery query)
    {
        try
        {
            var log = await auditLogRepository.FindByIdAsync(query.LogId);
            return log != null
                ? Result<object>.Success(log)
                : Result<object>.Failure(AdminError.AuditLogNotFound);
        }
        catch { return Result<object>.Failure(AdminError.AuditLogNotFound); }
    }

    public async Task<Result<object>> GetAuditLogsByUserAsync(GetAuditLogsByUserQuery query)
    {
        try
        {
            var logs = await auditLogRepository.FindByUserIdAsync(query.TargetUserId);
            return Result<object>.Success(new { Logs = logs, Total = logs.Count });
        }
        catch { return Result<object>.Failure(AdminError.AuditLogNotFound); }
    }

    public async Task<Result<object>> GetAuditLogsByEntityAsync(GetAuditLogsByEntityQuery query)
    {
        try
        {
            var logs = await auditLogRepository.FindByEntityAsync(query.EntityType, query.EntityId);
            return Result<object>.Success(new { Logs = logs, Total = logs.Count });
        }
        catch { return Result<object>.Failure(AdminError.AuditLogNotFound); }
    }

    public async Task<Result<object>> GetSystemSettingsAsync(GetSystemSettingsQuery query)
    {
        try
        {
            var settings = await systemSettingRepository.GetAllAsync();
            return Result<object>.Success(new { Settings = settings });
        }
        catch { return Result<object>.Failure(AdminError.SettingNotFound); }
    }

    public async Task<Result<object>> GetEmailSettingsAsync(GetEmailSettingsQuery query)
    {
        try
        {
            var settings = await systemSettingRepository.FindByCategoryAsync("email");
            var dict = settings.ToDictionary(s => s.Key, s => s.Value);
            return Result<object>.Success(dict);
        }
        catch { return Result<object>.Failure(AdminError.SettingNotFound); }
    }

    public async Task<Result<object>> GetNotificationSystemSettingsAsync(GetNotificationSystemSettingsQuery query)
    {
        try
        {
            var settings = await systemSettingRepository.FindByCategoryAsync("notifications");
            var dict = settings.ToDictionary(s => s.Key, s => s.Value);
            return Result<object>.Success(dict);
        }
        catch { return Result<object>.Failure(AdminError.SettingNotFound); }
    }

    public async Task<Result<object>> GetIntegrationSettingsAsync(GetIntegrationSettingsQuery query)
    {
        try
        {
            var settings = await systemSettingRepository.FindByCategoryAsync("integrations");
            var dict = settings.ToDictionary(s => s.Key, s => s.Value);
            return Result<object>.Success(dict);
        }
        catch { return Result<object>.Failure(AdminError.SettingNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<ContentPage>>> GetContentPagesAsync(GetContentPagesQuery query)
    {
        try
        {
            var pages = await contentPageRepository.GetAllAsync();
            return Result<IReadOnlyCollection<ContentPage>>.Success(pages);
        }
        catch { return Result<IReadOnlyCollection<ContentPage>>.Failure(AdminError.ContentPageNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<Faq>>> GetFaqsAsync(GetFaqsQuery query)
    {
        try
        {
            var faqs = await faqRepository.GetAllAsync();
            return Result<IReadOnlyCollection<Faq>>.Success(faqs);
        }
        catch { return Result<IReadOnlyCollection<Faq>>.Failure(AdminError.FaqNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<Announcement>>> GetAnnouncementsAsync(GetAnnouncementsQuery query)
    {
        try
        {
            var announcements = await announcementRepository.GetAllAsync();
            return Result<IReadOnlyCollection<Announcement>>.Success(announcements);
        }
        catch { return Result<IReadOnlyCollection<Announcement>>.Failure(AdminError.AnnouncementNotFound); }
    }
}
