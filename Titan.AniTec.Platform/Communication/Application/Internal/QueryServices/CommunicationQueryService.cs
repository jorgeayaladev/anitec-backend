using Titan.AniTec.Platform.Communication.Domain.Model;
using Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Communication.Domain.Repositories;
using Titan.AniTec.Platform.Communication.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Communication.Application.Internal.QueryServices;

public class CommunicationQueryService(
    IConversationRepository conversationRepository,
    IMessageRepository messageRepository,
    IContactRepository contactRepository,
    ISharedReportRepository sharedReportRepository,
    INotificationSettingRepository notificationSettingRepository) : ICommunicationQueryService
{
    public async Task<Result<IReadOnlyCollection<Conversation>>> GetConversationsAsync(GetConversationsQuery query)
    {
        try
        {
            var items = await conversationRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Conversation>>.Success(items);
        }
        catch { return Result<IReadOnlyCollection<Conversation>>.Failure(CommunicationError.ConversationNotFound); }
    }

    public async Task<Result<Conversation>> GetConversationByIdAsync(GetConversationByIdQuery query)
    {
        try
        {
            var item = await conversationRepository.FindByIdAsync(query.ConversationId);
            return item != null
                ? Result<Conversation>.Success(item)
                : Result<Conversation>.Failure(CommunicationError.ConversationNotFound);
        }
        catch { return Result<Conversation>.Failure(CommunicationError.ConversationNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<Message>>> GetConversationMessagesAsync(GetConversationMessagesQuery query)
    {
        try
        {
            var items = await conversationRepository.FindMessagesByConversationIdAsync(query.ConversationId);
            return Result<IReadOnlyCollection<Message>>.Success(items);
        }
        catch { return Result<IReadOnlyCollection<Message>>.Failure(CommunicationError.MessageNotFound); }
    }

    public async Task<Result<int>> GetUnreadCountAsync(GetUnreadCountQuery query)
    {
        try
        {
            var count = await conversationRepository.GetUnreadCountAsync(query.UserId);
            return Result<int>.Success(count);
        }
        catch { return Result<int>.Failure(CommunicationError.MessageNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<SharedReport>>> GetSharedReportsAsync(GetSharedReportsQuery query)
    {
        try
        {
            var items = await sharedReportRepository.FindBySharedWithIdAsync(query.UserId);
            return Result<IReadOnlyCollection<SharedReport>>.Success(items);
        }
        catch { return Result<IReadOnlyCollection<SharedReport>>.Failure(CommunicationError.SharedReportNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<SharedReport>>> GetSharedByMeReportsAsync(GetSharedByMeReportsQuery query)
    {
        try
        {
            var items = await sharedReportRepository.FindBySharedByIdAsync(query.UserId);
            return Result<IReadOnlyCollection<SharedReport>>.Success(items);
        }
        catch { return Result<IReadOnlyCollection<SharedReport>>.Failure(CommunicationError.SharedReportNotFound); }
    }

    public async Task<Result<SharedReport>> GetSharedReportViewAsync(GetSharedReportViewQuery query)
    {
        try
        {
            var item = await sharedReportRepository.FindByIdAsync(query.ShareId);
            return item != null && item.IsActive
                ? Result<SharedReport>.Success(item)
                : Result<SharedReport>.Failure(CommunicationError.SharedReportNotFound);
        }
        catch { return Result<SharedReport>.Failure(CommunicationError.SharedReportNotFound); }
    }

    public async Task<Result<NotificationSetting>> GetNotificationSettingsAsync(GetNotificationSettingsQuery query)
    {
        try
        {
            var item = await notificationSettingRepository.FindByFarmIdAsync(query.UserId);
            return item != null
                ? Result<NotificationSetting>.Success(item)
                : Result<NotificationSetting>.Failure(CommunicationError.NotificationSettingsNotFound);
        }
        catch { return Result<NotificationSetting>.Failure(CommunicationError.NotificationSettingsNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<Contact>>> GetContactsAsync(GetContactsQuery query)
    {
        try
        {
            var items = await contactRepository.FindByFarmIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Contact>>.Success(items);
        }
        catch { return Result<IReadOnlyCollection<Contact>>.Failure(CommunicationError.ContactNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<Contact>>> GetContactsSearchAsync(GetContactsSearchQuery query)
    {
        try
        {
            var items = await contactRepository.SearchAsync(query.UserId, query.SearchTerm);
            return Result<IReadOnlyCollection<Contact>>.Success(items);
        }
        catch { return Result<IReadOnlyCollection<Contact>>.Failure(CommunicationError.ContactNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<Contact>>> GetVeterinariansAsync(GetVeterinariansQuery query)
    {
        try
        {
            var items = await contactRepository.FindByRoleAsync(query.UserId, "veterinarian");
            return Result<IReadOnlyCollection<Contact>>.Success(items);
        }
        catch { return Result<IReadOnlyCollection<Contact>>.Failure(CommunicationError.ContactNotFound); }
    }

    public async Task<Result<IReadOnlyCollection<Contact>>> GetFarmersAsync(GetFarmersQuery query)
    {
        try
        {
            var items = await contactRepository.FindByRoleAsync(query.UserId, "farmer");
            return Result<IReadOnlyCollection<Contact>>.Success(items);
        }
        catch { return Result<IReadOnlyCollection<Contact>>.Failure(CommunicationError.ContactNotFound); }
    }
}
