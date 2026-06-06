namespace Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;

public class ContactRequest
{
    public ContactRequest(int farmId, int fromUserId, int toUserId)
    {
        FarmId = farmId;
        FromUserId = fromUserId;
        ToUserId = toUserId;
        Status = "pending";
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public int FromUserId { get; private set; }
    public int ToUserId { get; private set; }
    public string Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public ContactRequest Accept()
    {
        Status = "accepted";
        return this;
    }

    public ContactRequest Reject()
    {
        Status = "rejected";
        return this;
    }
}
