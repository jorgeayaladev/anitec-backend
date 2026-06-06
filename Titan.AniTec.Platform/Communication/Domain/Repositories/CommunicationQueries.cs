namespace Titan.AniTec.Platform.Communication.Domain.Repositories;

public record GetConversationsQuery(int UserId);
public record GetConversationByIdQuery(int UserId, int ConversationId);
public record GetConversationMessagesQuery(int UserId, int ConversationId);
public record GetUnreadCountQuery(int UserId);

public record GetSharedReportsQuery(int UserId);
public record GetSharedByMeReportsQuery(int UserId);
public record GetSharedReportViewQuery(int UserId, int ShareId);

public record GetNotificationSettingsQuery(int UserId);

public record GetContactsQuery(int UserId);
public record GetContactsSearchQuery(int UserId, string SearchTerm);
public record GetVeterinariansQuery(int UserId);
public record GetFarmersQuery(int UserId);
