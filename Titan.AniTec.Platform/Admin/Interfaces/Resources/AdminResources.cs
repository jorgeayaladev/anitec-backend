namespace Titan.AniTec.Platform.Admin.Interfaces.Resources;

public record SystemSettingResource(string Key, string Value);
public record UpdateSystemSettingResource(string Value);

public record EmailSettingsResource(string SmtpHost, int SmtpPort, string Username, string Password, string FromAddress, string FromName);

public record NotificationSystemSettingsResource(bool EmailEnabled, bool PushEnabled, bool SmsEnabled);

public record StripeConfigResource(string SecretKey, string PublishableKey, string WebhookSecret);
public record IotConfigResource(string MqttBrokerUrl, int MqttPort, string MqttUsername, string MqttPassword);

public record ContentPageResource(int Id, string Slug, string Title, string Content, DateTimeOffset UpdatedAt);
public record UpdateContentPageResource(string Title, string Content);

public record FaqResource(int Id, string Question, string Answer, int SortOrder);
public record CreateFaqResource(string Question, string Answer, int SortOrder);
public record UpdateFaqResource(string Question, string Answer, int SortOrder);

public record AnnouncementResource(int Id, string Title, string Content, string Severity, bool IsActive, DateTimeOffset CreatedAt);
public record CreateAnnouncementResource(string Title, string Content, string? Severity);
public record UpdateAnnouncementResource(string Title, string Content, string? Severity);
