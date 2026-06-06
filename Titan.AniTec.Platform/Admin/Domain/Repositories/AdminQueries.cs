namespace Titan.AniTec.Platform.Admin.Domain.Repositories;

public record GetAdminDashboardQuery(int UserId);
public record GetAdminStatisticsQuery(int UserId);
public record GetAuditLogsQuery(int UserId);
public record GetAuditLogByIdQuery(int UserId, int LogId);
public record GetAuditLogsByUserQuery(int UserId, int TargetUserId);
public record GetAuditLogsByEntityQuery(int UserId, string EntityType, int EntityId);

public record GetSystemSettingsQuery(int UserId);
public record GetEmailSettingsQuery(int UserId);
public record GetNotificationSystemSettingsQuery(int UserId);
public record GetIntegrationSettingsQuery(int UserId);

public record GetContentPagesQuery(int UserId);
public record GetFaqsQuery(int UserId);
public record GetAnnouncementsQuery(int UserId);
