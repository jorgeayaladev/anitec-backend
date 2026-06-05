using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;

public class UserPreferences : IAuditableEntity
{
    public UserPreferences(int userId)
    {
        UserId = userId;
        Language = "en";
        Theme = "light";
        NotificationsEnabled = true;
        EmailNotifications = true;
        PushNotifications = true;
        SmsNotifications = false;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Language { get; private set; }
    public string Theme { get; private set; }
    public bool NotificationsEnabled { get; private set; }
    public bool EmailNotifications { get; private set; }
    public bool PushNotifications { get; private set; }
    public bool SmsNotifications { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public UserPreferences Update(string? language, string? theme)
    {
        if (language != null) Language = language;
        if (theme != null) Theme = theme;
        return this;
    }

    public UserPreferences UpdateNotifications(bool? notificationsEnabled, bool? emailNotifications,
        bool? pushNotifications, bool? smsNotifications)
    {
        if (notificationsEnabled.HasValue) NotificationsEnabled = notificationsEnabled.Value;
        if (emailNotifications.HasValue) EmailNotifications = emailNotifications.Value;
        if (pushNotifications.HasValue) PushNotifications = pushNotifications.Value;
        if (smsNotifications.HasValue) SmsNotifications = smsNotifications.Value;
        return this;
    }
}
