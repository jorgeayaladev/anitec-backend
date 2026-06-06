using Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Communication.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Communication.Application.CommandServices;

public interface ICommunicationCommandService
{
    Task<Result<Conversation>> CreateConversationAsync(CreateConversationCommand command);
    Task<Result<Conversation>> UpdateConversationAsync(UpdateConversationCommand command);
    Task<Result> ArchiveConversationAsync(ArchiveConversationCommand command);
    Task<Result<Message>> SendMessageAsync(SendMessageCommand command);
    Task<Result> MarkMessageAsReadAsync(MarkMessageAsReadCommand command);
    Task<Result> DeleteMessageAsync(DeleteMessageCommand command);
    Task<Result> MarkAllAsReadAsync(MarkAllAsReadCommand command);
    Task<Result<SharedReport>> ShareReportAsync(ShareReportCommand command);
    Task<Result> RevokeSharedReportAsync(RevokeSharedReportCommand command);
    Task<Result<PushDevice>> RegisterPushDeviceAsync(RegisterPushDeviceCommand command);
    Task<Result> UnregisterPushDeviceAsync(UnregisterPushDeviceCommand command);
    Task<Result<NotificationSetting>> UpdateNotificationSettingsAsync(UpdateNotificationSettingsCommand command);
    Task<Result<Contact>> AddContactAsync(AddContactCommand command);
    Task<Result<Contact>> UpdateContactAsync(UpdateContactCommand command);
    Task<Result> DeleteContactAsync(DeleteContactCommand command);
    Task<Result<ContactRequest>> SendContactRequestAsync(SendContactRequestCommand command);
    Task<Result<ContactRequest>> AcceptContactRequestAsync(AcceptContactRequestCommand command);
    Task<Result<ContactRequest>> RejectContactRequestAsync(RejectContactRequestCommand command);
}

public interface ICommunicationQueryService
{
    Task<Result<IReadOnlyCollection<Conversation>>> GetConversationsAsync(GetConversationsQuery query);
    Task<Result<Conversation>> GetConversationByIdAsync(GetConversationByIdQuery query);
    Task<Result<IReadOnlyCollection<Message>>> GetConversationMessagesAsync(GetConversationMessagesQuery query);
    Task<Result<int>> GetUnreadCountAsync(GetUnreadCountQuery query);
    Task<Result<IReadOnlyCollection<SharedReport>>> GetSharedReportsAsync(GetSharedReportsQuery query);
    Task<Result<IReadOnlyCollection<SharedReport>>> GetSharedByMeReportsAsync(GetSharedByMeReportsQuery query);
    Task<Result<SharedReport>> GetSharedReportViewAsync(GetSharedReportViewQuery query);
    Task<Result<NotificationSetting>> GetNotificationSettingsAsync(GetNotificationSettingsQuery query);
    Task<Result<IReadOnlyCollection<Contact>>> GetContactsAsync(GetContactsQuery query);
    Task<Result<IReadOnlyCollection<Contact>>> GetContactsSearchAsync(GetContactsSearchQuery query);
    Task<Result<IReadOnlyCollection<Contact>>> GetVeterinariansAsync(GetVeterinariansQuery query);
    Task<Result<IReadOnlyCollection<Contact>>> GetFarmersAsync(GetFarmersQuery query);
}
