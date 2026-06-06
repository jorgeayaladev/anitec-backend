using Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Communication.Domain.Repositories;

public interface IConversationRepository : IBaseRepository<Conversation>
{
    Task<IReadOnlyCollection<Conversation>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Message>> FindMessagesByConversationIdAsync(int conversationId);
    Task<int> GetUnreadCountAsync(int farmId);
    Task MarkAllAsReadAsync(int conversationId, int userId);
}

public interface IMessageRepository : IBaseRepository<Message>
{
    Task<IReadOnlyCollection<Message>> FindByConversationIdAsync(int conversationId);
}

public interface IContactRepository : IBaseRepository<Contact>
{
    Task<IReadOnlyCollection<Contact>> FindByFarmIdAsync(int farmId);
    Task<IReadOnlyCollection<Contact>> SearchAsync(int farmId, string searchTerm);
    Task<IReadOnlyCollection<Contact>> FindByRoleAsync(int farmId, string role);
}

public interface IContactRequestRepository : IBaseRepository<ContactRequest>
{
    Task<IReadOnlyCollection<ContactRequest>> FindByToUserIdAsync(int toUserId);
    Task<IReadOnlyCollection<ContactRequest>> FindByFromUserIdAsync(int fromUserId);
}

public interface ISharedReportRepository : IBaseRepository<SharedReport>
{
    Task<IReadOnlyCollection<SharedReport>> FindBySharedWithIdAsync(int sharedWithId);
    Task<IReadOnlyCollection<SharedReport>> FindBySharedByIdAsync(int sharedById);
}

public interface IPushDeviceRepository : IBaseRepository<PushDevice>
{
    Task<PushDevice?> FindByFarmIdAsync(int farmId);
    Task<PushDevice?> FindByDeviceTokenAsync(string deviceToken);
}

public interface INotificationSettingRepository : IBaseRepository<NotificationSetting>
{
    Task<NotificationSetting?> FindByFarmIdAsync(int farmId);
}
