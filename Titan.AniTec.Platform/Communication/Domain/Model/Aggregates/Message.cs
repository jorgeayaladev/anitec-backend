namespace Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;

public class Message
{
    public Message(int conversationId, int senderId, string content)
    {
        ConversationId = conversationId;
        SenderId = senderId;
        Content = content;
    }

    public int Id { get; private set; }
    public int ConversationId { get; private set; }
    public int SenderId { get; private set; }
    public string Content { get; private set; }
    public DateTimeOffset SentAt { get; private set; }
    public bool IsRead { get; private set; }
    public DateTimeOffset? ReadAt { get; private set; }

    public Message MarkAsRead()
    {
        IsRead = true;
        ReadAt = DateTimeOffset.UtcNow;
        return this;
    }
}
