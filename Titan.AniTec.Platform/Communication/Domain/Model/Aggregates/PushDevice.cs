namespace Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;

public class PushDevice
{
    public PushDevice(int farmId, string deviceToken, string platform)
    {
        FarmId = farmId;
        DeviceToken = deviceToken;
        Platform = platform;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string DeviceToken { get; private set; }
    public string Platform { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
}
