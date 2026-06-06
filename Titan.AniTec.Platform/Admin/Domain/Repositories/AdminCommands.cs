namespace Titan.AniTec.Platform.Admin.Domain.Repositories;

public record UpdateSystemSettingCommand(int UserId, string Key, string Value);
public record UpdateEmailSettingsCommand(int UserId, string SmtpHost, int SmtpPort, string Username, string Password, string FromAddress, string FromName);
public record UpdateNotificationSettingsCommand(int UserId, bool EmailEnabled, bool PushEnabled, bool SmsEnabled);
public record ConfigureStripeCommand(int UserId, string SecretKey, string PublishableKey, string WebhookSecret);
public record ConfigureIotCommand(int UserId, string MqttBrokerUrl, int MqttPort, string MqttUsername, string MqttPassword);

public record UpdateContentPageCommand(int UserId, int PageId, string Title, string Content);
public record CreateFaqCommand(int UserId, string Question, string Answer, int SortOrder);
public record UpdateFaqCommand(int UserId, int FaqId, string Question, string Answer, int SortOrder);
public record DeleteFaqCommand(int UserId, int FaqId);

public record CreateAnnouncementCommand(int UserId, string Title, string Content, string? Severity);
public record UpdateAnnouncementCommand(int UserId, int AnnouncementId, string Title, string Content, string? Severity);
public record DeleteAnnouncementCommand(int UserId, int AnnouncementId);
