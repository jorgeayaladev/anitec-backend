using Titan.AniTec.Platform.Communication.Domain.Model;
using Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Communication.Domain.Repositories;
using Titan.AniTec.Platform.Communication.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Communication.Application.Internal.CommandServices;

public class CommunicationCommandService(
    IConversationRepository conversationRepository,
    IMessageRepository messageRepository,
    IContactRepository contactRepository,
    IContactRequestRepository contactRequestRepository,
    ISharedReportRepository sharedReportRepository,
    IPushDeviceRepository pushDeviceRepository,
    INotificationSettingRepository notificationSettingRepository,
    IUnitOfWork unitOfWork) : ICommunicationCommandService
{
    public async Task<Result<Conversation>> CreateConversationAsync(CreateConversationCommand command)
    {
        try
        {
            var conversation = new Conversation(command.UserId, command.Title, command.Participants);
            await conversationRepository.AddAsync(conversation);
            await unitOfWork.CompleteAsync();
            return Result<Conversation>.Success(conversation);
        }
        catch { return Result<Conversation>.Failure(CommunicationError.InvalidConversationData); }
    }

    public async Task<Result<Conversation>> UpdateConversationAsync(UpdateConversationCommand command)
    {
        try
        {
            var conversation = await conversationRepository.FindByIdAsync(command.ConversationId);
            if (conversation == null) return Result<Conversation>.Failure(CommunicationError.ConversationNotFound);
            conversation.UpdateDetails(command.Title, command.Participants);
            await unitOfWork.CompleteAsync();
            return Result<Conversation>.Success(conversation);
        }
        catch { return Result<Conversation>.Failure(CommunicationError.InvalidConversationData); }
    }

    public async Task<Result> ArchiveConversationAsync(ArchiveConversationCommand command)
    {
        try
        {
            var conversation = await conversationRepository.FindByIdAsync(command.ConversationId);
            if (conversation == null) return Result.Failure(CommunicationError.ConversationNotFound);
            conversation.MarkAsArchived();
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(CommunicationError.ConversationNotFound); }
    }

    public async Task<Result<Message>> SendMessageAsync(SendMessageCommand command)
    {
        try
        {
            var conversation = await conversationRepository.FindByIdAsync(command.ConversationId);
            if (conversation == null) return Result<Message>.Failure(CommunicationError.ConversationNotFound);

            var message = new Message(command.ConversationId, command.UserId, command.Content);
            await messageRepository.AddAsync(message);
            await unitOfWork.CompleteAsync();
            return Result<Message>.Success(message);
        }
        catch { return Result<Message>.Failure(CommunicationError.InvalidMessageData); }
    }

    public async Task<Result> MarkMessageAsReadAsync(MarkMessageAsReadCommand command)
    {
        try
        {
            var message = await messageRepository.FindByIdAsync(command.MessageId);
            if (message == null) return Result.Failure(CommunicationError.MessageNotFound);
            message.MarkAsRead();
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(CommunicationError.MessageNotFound); }
    }

    public async Task<Result> DeleteMessageAsync(DeleteMessageCommand command)
    {
        try
        {
            var message = await messageRepository.FindByIdAsync(command.MessageId);
            if (message == null) return Result.Failure(CommunicationError.MessageNotFound);
            messageRepository.Remove(message);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(CommunicationError.MessageNotFound); }
    }

    public async Task<Result> MarkAllAsReadAsync(MarkAllAsReadCommand command)
    {
        try
        {
            await conversationRepository.MarkAllAsReadAsync(command.ConversationId, command.UserId);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(CommunicationError.MessageNotFound); }
    }

    public async Task<Result<SharedReport>> ShareReportAsync(ShareReportCommand command)
    {
        try
        {
            var report = new SharedReport(command.ReportId, command.UserId, command.SharedWithId);
            await sharedReportRepository.AddAsync(report);
            await unitOfWork.CompleteAsync();
            return Result<SharedReport>.Success(report);
        }
        catch { return Result<SharedReport>.Failure(CommunicationError.InvalidSharedReportData); }
    }

    public async Task<Result> RevokeSharedReportAsync(RevokeSharedReportCommand command)
    {
        try
        {
            var report = await sharedReportRepository.FindByIdAsync(command.ShareId);
            if (report == null) return Result.Failure(CommunicationError.SharedReportNotFound);
            report.RevokeAccess();
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(CommunicationError.SharedReportNotFound); }
    }

    public async Task<Result<PushDevice>> RegisterPushDeviceAsync(RegisterPushDeviceCommand command)
    {
        try
        {
            var device = new PushDevice(command.UserId, command.DeviceToken, command.Platform);
            await pushDeviceRepository.AddAsync(device);
            await unitOfWork.CompleteAsync();
            return Result<PushDevice>.Success(device);
        }
        catch { return Result<PushDevice>.Failure(CommunicationError.InvalidPushDeviceData); }
    }

    public async Task<Result> UnregisterPushDeviceAsync(UnregisterPushDeviceCommand command)
    {
        try
        {
            var device = await pushDeviceRepository.FindByIdAsync(command.DeviceId);
            if (device == null) return Result.Failure(CommunicationError.PushDeviceNotFound);
            pushDeviceRepository.Remove(device);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(CommunicationError.PushDeviceNotFound); }
    }

    public async Task<Result<NotificationSetting>> UpdateNotificationSettingsAsync(UpdateNotificationSettingsCommand command)
    {
        try
        {
            var settings = await notificationSettingRepository.FindByFarmIdAsync(command.UserId);
            if (settings == null)
            {
                settings = new NotificationSetting(command.UserId, command.NotificationsEnabled, command.Settings);
                await notificationSettingRepository.AddAsync(settings);
            }
            else
            {
                settings.Update(command.NotificationsEnabled, command.Settings);
            }
            await unitOfWork.CompleteAsync();
            return Result<NotificationSetting>.Success(settings);
        }
        catch { return Result<NotificationSetting>.Failure(CommunicationError.InvalidNotificationSettingsData); }
    }

    public async Task<Result<Contact>> AddContactAsync(AddContactCommand command)
    {
        try
        {
            var contact = new Contact(command.UserId, command.ContactUserId, command.Name,
                command.Email, command.Phone, command.Role, command.Notes);
            await contactRepository.AddAsync(contact);
            await unitOfWork.CompleteAsync();
            return Result<Contact>.Success(contact);
        }
        catch { return Result<Contact>.Failure(CommunicationError.InvalidContactData); }
    }

    public async Task<Result<Contact>> UpdateContactAsync(UpdateContactCommand command)
    {
        try
        {
            var contact = await contactRepository.FindByIdAsync(command.ContactId);
            if (contact == null) return Result<Contact>.Failure(CommunicationError.ContactNotFound);
            contact.UpdateDetails(command.Name, command.Email, command.Phone, command.Role, command.Notes);
            await unitOfWork.CompleteAsync();
            return Result<Contact>.Success(contact);
        }
        catch { return Result<Contact>.Failure(CommunicationError.InvalidContactData); }
    }

    public async Task<Result> DeleteContactAsync(DeleteContactCommand command)
    {
        try
        {
            var contact = await contactRepository.FindByIdAsync(command.ContactId);
            if (contact == null) return Result.Failure(CommunicationError.ContactNotFound);
            contactRepository.Remove(contact);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(CommunicationError.ContactNotFound); }
    }

    public async Task<Result<ContactRequest>> SendContactRequestAsync(SendContactRequestCommand command)
    {
        try
        {
            var request = new ContactRequest(command.UserId, command.UserId, command.ToUserId);
            await contactRequestRepository.AddAsync(request);
            await unitOfWork.CompleteAsync();
            return Result<ContactRequest>.Success(request);
        }
        catch { return Result<ContactRequest>.Failure(CommunicationError.InvalidContactRequestData); }
    }

    public async Task<Result<ContactRequest>> AcceptContactRequestAsync(AcceptContactRequestCommand command)
    {
        try
        {
            var request = await contactRequestRepository.FindByIdAsync(command.RequestId);
            if (request == null) return Result<ContactRequest>.Failure(CommunicationError.ContactRequestNotFound);
            request.Accept();
            await unitOfWork.CompleteAsync();
            return Result<ContactRequest>.Success(request);
        }
        catch { return Result<ContactRequest>.Failure(CommunicationError.InvalidContactRequestData); }
    }

    public async Task<Result<ContactRequest>> RejectContactRequestAsync(RejectContactRequestCommand command)
    {
        try
        {
            var request = await contactRequestRepository.FindByIdAsync(command.RequestId);
            if (request == null) return Result<ContactRequest>.Failure(CommunicationError.ContactRequestNotFound);
            request.Reject();
            await unitOfWork.CompleteAsync();
            return Result<ContactRequest>.Success(request);
        }
        catch { return Result<ContactRequest>.Failure(CommunicationError.InvalidContactRequestData); }
    }
}
