namespace Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;

public class NotificationSetting
{
    public NotificationSetting(int farmId, bool notificationsEnabled, string settings)
    {
        FarmId = farmId;
        NotificationsEnabled = notificationsEnabled;
        Settings = settings;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public bool NotificationsEnabled { get; private set; }
    public string Settings { get; private set; }

    public NotificationSetting Update(bool notificationsEnabled, string settings)
    {
        NotificationsEnabled = notificationsEnabled;
        Settings = settings;
        return this;
    }
}
