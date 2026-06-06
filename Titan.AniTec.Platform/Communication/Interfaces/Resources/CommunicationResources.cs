namespace Titan.AniTec.Platform.Communication.Interfaces.Resources;

public record ConversationResource(int Id, int FarmId, string Title, string Participants, bool IsArchived, DateTimeOffset CreatedAt);
public record CreateConversationResource(string Title, string Participants);
public record UpdateConversationResource(string Title, string Participants);

public record MessageResource(int Id, int ConversationId, int SenderId, string Content, DateTimeOffset SentAt, bool IsRead, DateTimeOffset? ReadAt);
public record SendMessageResource(string Content);

public record UnreadCountResource(int Count);

public record SharedReportResource(int Id, int ReportId, int SharedById, int SharedWithId, bool IsActive, DateTimeOffset SharedAt);
public record ShareReportResource(int SharedWithId);

public record PushDeviceResource(int Id, int FarmId, string DeviceToken, string Platform, DateTimeOffset CreatedAt);
public record RegisterPushDeviceResource(string DeviceToken, string Platform);

public record NotificationSettingResource(int Id, int FarmId, bool NotificationsEnabled, string Settings);
public record UpdateNotificationSettingsResource(bool NotificationsEnabled, string Settings);

public record ContactResource(int Id, int FarmId, int ContactUserId, string Name, string? Email, string? Phone, string Role, string? Notes, DateTimeOffset CreatedAt);
public record CreateContactResource(int ContactUserId, string Name, string? Email, string? Phone, string Role, string? Notes);
public record UpdateContactResource(string Name, string? Email, string? Phone, string Role, string? Notes);

public record ContactRequestResource(int Id, int FarmId, int FromUserId, int ToUserId, string Status, DateTimeOffset CreatedAt);
public record SendContactRequestResource(int ToUserId);
