using Titan.AniTec.Platform.Admin.Domain.Model;
using Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Admin.Domain.Repositories;
using Titan.AniTec.Platform.Admin.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Admin.Application.Internal.CommandServices;

public class AdminCommandService(
    ISystemSettingRepository systemSettingRepository,
    IFaqRepository faqRepository,
    IAnnouncementRepository announcementRepository,
    IContentPageRepository contentPageRepository,
    IUnitOfWork unitOfWork) : IAdminCommandService
{
    public async Task<Result> UpdateSystemSettingAsync(UpdateSystemSettingCommand command)
    {
        try
        {
            var setting = await systemSettingRepository.FindByKeyAsync(command.Key);
            if (setting == null)
            {
                setting = new SystemSetting(command.Key, command.Value, "general");
                await systemSettingRepository.AddAsync(setting);
            }
            else setting.UpdateValue(command.Value);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(AdminError.InvalidSettingData); }
    }

    public async Task<Result> UpdateEmailSettingsAsync(UpdateEmailSettingsCommand command)
    {
        try
        {
            await UpsertSettingAsync("smtp_host", command.SmtpHost, "email");
            await UpsertSettingAsync("smtp_port", command.SmtpPort.ToString(), "email");
            await UpsertSettingAsync("smtp_username", command.Username, "email");
            await UpsertSettingAsync("smtp_password", command.Password, "email");
            await UpsertSettingAsync("smtp_from_address", command.FromAddress, "email");
            await UpsertSettingAsync("smtp_from_name", command.FromName, "email");
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(AdminError.InvalidSettingData); }
    }

    public async Task<Result> UpdateNotificationSystemSettingsAsync(UpdateNotificationSettingsCommand command)
    {
        try
        {
            await UpsertSettingAsync("notifications_email", command.EmailEnabled.ToString(), "notifications");
            await UpsertSettingAsync("notifications_push", command.PushEnabled.ToString(), "notifications");
            await UpsertSettingAsync("notifications_sms", command.SmsEnabled.ToString(), "notifications");
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(AdminError.InvalidSettingData); }
    }

    public async Task<Result> ConfigureStripeAsync(ConfigureStripeCommand command)
    {
        try
        {
            await UpsertSettingAsync("stripe_secret_key", command.SecretKey, "integrations");
            await UpsertSettingAsync("stripe_publishable_key", command.PublishableKey, "integrations");
            await UpsertSettingAsync("stripe_webhook_secret", command.WebhookSecret, "integrations");
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(AdminError.InvalidSettingData); }
    }

    public async Task<Result> ConfigureIotAsync(ConfigureIotCommand command)
    {
        try
        {
            await UpsertSettingAsync("iot_mqtt_broker_url", command.MqttBrokerUrl, "integrations");
            await UpsertSettingAsync("iot_mqtt_port", command.MqttPort.ToString(), "integrations");
            await UpsertSettingAsync("iot_mqtt_username", command.MqttUsername, "integrations");
            await UpsertSettingAsync("iot_mqtt_password", command.MqttPassword, "integrations");
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(AdminError.InvalidSettingData); }
    }

    public async Task<Result> UpdateContentPageAsync(UpdateContentPageCommand command)
    {
        try
        {
            var page = await contentPageRepository.FindByIdAsync(command.PageId);
            if (page == null) return Result.Failure(AdminError.ContentPageNotFound);
            page.Update(command.Title, command.Content);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(AdminError.ContentPageNotFound); }
    }

    public async Task<Result<Faq>> CreateFaqAsync(CreateFaqCommand command)
    {
        try
        {
            var faq = new Faq(command.Question, command.Answer, command.SortOrder);
            await faqRepository.AddAsync(faq);
            await unitOfWork.CompleteAsync();
            return Result<Faq>.Success(faq);
        }
        catch { return Result<Faq>.Failure(AdminError.InvalidContentData); }
    }

    public async Task<Result<Faq>> UpdateFaqAsync(UpdateFaqCommand command)
    {
        try
        {
            var faq = await faqRepository.FindByIdAsync(command.FaqId);
            if (faq == null) return Result<Faq>.Failure(AdminError.FaqNotFound);
            faq.Update(command.Question, command.Answer, command.SortOrder);
            await unitOfWork.CompleteAsync();
            return Result<Faq>.Success(faq);
        }
        catch { return Result<Faq>.Failure(AdminError.InvalidContentData); }
    }

    public async Task<Result> DeleteFaqAsync(DeleteFaqCommand command)
    {
        try
        {
            var faq = await faqRepository.FindByIdAsync(command.FaqId);
            if (faq == null) return Result.Failure(AdminError.FaqNotFound);
            faqRepository.Remove(faq);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(AdminError.FaqNotFound); }
    }

    public async Task<Result<Announcement>> CreateAnnouncementAsync(CreateAnnouncementCommand command)
    {
        try
        {
            var announcement = new Announcement(command.Title, command.Content, command.Severity);
            await announcementRepository.AddAsync(announcement);
            await unitOfWork.CompleteAsync();
            return Result<Announcement>.Success(announcement);
        }
        catch { return Result<Announcement>.Failure(AdminError.InvalidContentData); }
    }

    public async Task<Result<Announcement>> UpdateAnnouncementAsync(UpdateAnnouncementCommand command)
    {
        try
        {
            var announcement = await announcementRepository.FindByIdAsync(command.AnnouncementId);
            if (announcement == null) return Result<Announcement>.Failure(AdminError.AnnouncementNotFound);
            announcement.Update(command.Title, command.Content, command.Severity);
            await unitOfWork.CompleteAsync();
            return Result<Announcement>.Success(announcement);
        }
        catch { return Result<Announcement>.Failure(AdminError.InvalidContentData); }
    }

    public async Task<Result> DeleteAnnouncementAsync(DeleteAnnouncementCommand command)
    {
        try
        {
            var announcement = await announcementRepository.FindByIdAsync(command.AnnouncementId);
            if (announcement == null) return Result.Failure(AdminError.AnnouncementNotFound);
            announcementRepository.Remove(announcement);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch { return Result.Failure(AdminError.AnnouncementNotFound); }
    }

    private async Task UpsertSettingAsync(string key, string value, string category)
    {
        var existing = await systemSettingRepository.FindByKeyAsync(key);
        if (existing == null) await systemSettingRepository.AddAsync(new SystemSetting(key, value, category));
        else existing.UpdateValue(value);
    }
}
