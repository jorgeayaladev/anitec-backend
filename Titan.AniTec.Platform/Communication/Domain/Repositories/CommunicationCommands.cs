namespace Titan.AniTec.Platform.Communication.Domain.Repositories;

public record CreateConversationCommand(int UserId, string Title, string Participants);
public record UpdateConversationCommand(int UserId, int ConversationId, string Title, string Participants);
public record ArchiveConversationCommand(int UserId, int ConversationId);

public record SendMessageCommand(int UserId, int ConversationId, string Content);
public record MarkMessageAsReadCommand(int UserId, int MessageId);
public record DeleteMessageCommand(int UserId, int MessageId);
public record MarkAllAsReadCommand(int UserId, int ConversationId);

public record ShareReportCommand(int UserId, int ReportId, int SharedWithId);
public record RevokeSharedReportCommand(int UserId, int ShareId);

public record RegisterPushDeviceCommand(int UserId, string DeviceToken, string Platform);
public record UnregisterPushDeviceCommand(int UserId, int DeviceId);
public record UpdateNotificationSettingsCommand(int UserId, bool NotificationsEnabled, string Settings);

public record AddContactCommand(int UserId, int ContactUserId, string Name, string? Email, string? Phone, string Role, string? Notes);
public record UpdateContactCommand(int UserId, int ContactId, string Name, string? Email, string? Phone, string Role, string? Notes);
public record DeleteContactCommand(int UserId, int ContactId);

public record SendContactRequestCommand(int UserId, int ToUserId);
public record AcceptContactRequestCommand(int UserId, int RequestId);
public record RejectContactRequestCommand(int UserId, int RequestId);
