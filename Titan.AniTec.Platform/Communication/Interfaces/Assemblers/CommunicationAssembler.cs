using Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Communication.Domain.Repositories;
using Titan.AniTec.Platform.Communication.Interfaces.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Communication.Interfaces.Assemblers;

public static class CommunicationAssembler
{
    public static ConversationResource ToResource(this Conversation entity) =>
        new(entity.Id, entity.FarmId, entity.Title, entity.Participants, entity.IsArchived, entity.CreatedAt);

    public static CreateConversationCommand ToCommand(int farmId, CreateConversationResource resource) =>
        new(farmId, resource.Title, resource.Participants);

    public static UpdateConversationCommand ToCommand(int farmId, int conversationId, UpdateConversationResource resource) =>
        new(farmId, conversationId, resource.Title, resource.Participants);

    public static MessageResource ToResource(this Message entity) =>
        new(entity.Id, entity.ConversationId, entity.SenderId, entity.Content, entity.SentAt, entity.IsRead, entity.ReadAt);

    public static SendMessageCommand ToCommand(int farmId, int conversationId, SendMessageResource resource) =>
        new(farmId, conversationId, resource.Content);

    public static SharedReportResource ToResource(this SharedReport entity) =>
        new(entity.Id, entity.ReportId, entity.SharedById, entity.SharedWithId, entity.IsActive, entity.SharedAt);

    public static ShareReportCommand ToCommand(int farmId, int reportId, ShareReportResource resource) =>
        new(farmId, reportId, resource.SharedWithId);

    public static PushDeviceResource ToResource(this PushDevice entity) =>
        new(entity.Id, entity.FarmId, entity.DeviceToken, entity.Platform, entity.CreatedAt);

    public static RegisterPushDeviceCommand ToCommand(int farmId, RegisterPushDeviceResource resource) =>
        new(farmId, resource.DeviceToken, resource.Platform);

    public static NotificationSettingResource ToResource(this NotificationSetting entity) =>
        new(entity.Id, entity.FarmId, entity.NotificationsEnabled, entity.Settings);

    public static UpdateNotificationSettingsCommand ToCommand(int farmId, UpdateNotificationSettingsResource resource) =>
        new(farmId, resource.NotificationsEnabled, resource.Settings);

    public static ContactResource ToResource(this Contact entity) =>
        new(entity.Id, entity.FarmId, entity.ContactUserId, entity.Name, entity.Email, entity.Phone, entity.Role, entity.Notes, entity.CreatedAt);

    public static AddContactCommand ToCommand(int farmId, CreateContactResource resource) =>
        new(farmId, resource.ContactUserId, resource.Name, resource.Email, resource.Phone, resource.Role, resource.Notes);

    public static UpdateContactCommand ToCommand(int farmId, int contactId, UpdateContactResource resource) =>
        new(farmId, contactId, resource.Name, resource.Email, resource.Phone, resource.Role, resource.Notes);

    public static ContactRequestResource ToResource(this ContactRequest entity) =>
        new(entity.Id, entity.FarmId, entity.FromUserId, entity.ToUserId, entity.Status, entity.CreatedAt);

    public static SendContactRequestCommand ToCommand(int farmId, SendContactRequestResource resource) =>
        new(farmId, resource.ToUserId);
}

public static class CommunicationActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess) return new OkObjectResult(result.Data);
        return MapError(result.Error);
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess) return new OkResult();
        return MapError(result.Error);
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess) return new CreatedResult(string.Empty, result.Data);
        return MapError(result.Error);
    }

    private static IActionResult MapError(Enum error) => error switch
    {
        CommunicationError.ConversationNotFound or
        CommunicationError.MessageNotFound or
        CommunicationError.ContactNotFound or
        CommunicationError.ContactRequestNotFound or
        CommunicationError.SharedReportNotFound or
        CommunicationError.PushDeviceNotFound or
        CommunicationError.NotificationSettingsNotFound
            => new NotFoundObjectResult(new { Error = error.ToString() }),

        CommunicationError.InvalidConversationData or
        CommunicationError.InvalidMessageData or
        CommunicationError.InvalidContactData or
        CommunicationError.InvalidContactRequestData or
        CommunicationError.InvalidSharedReportData or
        CommunicationError.InvalidPushDeviceData or
        CommunicationError.InvalidNotificationSettingsData
            => new BadRequestObjectResult(new { Error = error.ToString() }),

        _ => new StatusCodeResult(500)
    };
}
