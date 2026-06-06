using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Communication.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Communication.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Titan.AniTec.Platform.Communication.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ConversationRepository(AppDbContext context) : BaseRepository<Conversation>(context), IConversationRepository
{
    public async Task<IReadOnlyCollection<Conversation>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Conversation>()
            .Where(c => c.FarmId == farmId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Message>> FindMessagesByConversationIdAsync(int conversationId)
        => await Context.Set<Message>()
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();

    public async Task<int> GetUnreadCountAsync(int farmId)
        => await Context.Set<Conversation>()
            .Where(c => c.FarmId == farmId)
            .Join(Context.Set<Message>(), c => c.Id, m => m.ConversationId, (c, m) => m)
            .CountAsync(m => !m.IsRead);

    public async Task MarkAllAsReadAsync(int conversationId, int userId)
    {
        var messages = await Context.Set<Message>()
            .Where(m => m.ConversationId == conversationId && m.SenderId != userId && !m.IsRead)
            .ToListAsync();
        foreach (var msg in messages)
            msg.MarkAsRead();
    }
}

public class MessageRepository(AppDbContext context) : BaseRepository<Message>(context), IMessageRepository
{
    public async Task<IReadOnlyCollection<Message>> FindByConversationIdAsync(int conversationId)
        => await Context.Set<Message>()
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
}

public class ContactRepository(AppDbContext context) : BaseRepository<Contact>(context), IContactRepository
{
    public async Task<IReadOnlyCollection<Contact>> FindByFarmIdAsync(int farmId)
        => await Context.Set<Contact>()
            .Where(c => c.FarmId == farmId)
            .OrderBy(c => c.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Contact>> SearchAsync(int farmId, string searchTerm)
        => await Context.Set<Contact>()
            .Where(c => c.FarmId == farmId && (c.Name.Contains(searchTerm) || c.Email!.Contains(searchTerm)))
            .OrderBy(c => c.Name)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Contact>> FindByRoleAsync(int farmId, string role)
        => await Context.Set<Contact>()
            .Where(c => c.FarmId == farmId && c.Role == role)
            .OrderBy(c => c.Name)
            .ToListAsync();
}

public class ContactRequestRepository(AppDbContext context) : BaseRepository<ContactRequest>(context), IContactRequestRepository
{
    public async Task<IReadOnlyCollection<ContactRequest>> FindByToUserIdAsync(int toUserId)
        => await Context.Set<ContactRequest>()
            .Where(r => r.ToUserId == toUserId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<ContactRequest>> FindByFromUserIdAsync(int fromUserId)
        => await Context.Set<ContactRequest>()
            .Where(r => r.FromUserId == fromUserId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
}

public class SharedReportRepository(AppDbContext context) : BaseRepository<SharedReport>(context), ISharedReportRepository
{
    public async Task<IReadOnlyCollection<SharedReport>> FindBySharedWithIdAsync(int sharedWithId)
        => await Context.Set<SharedReport>()
            .Where(r => r.SharedWithId == sharedWithId && r.IsActive)
            .OrderByDescending(r => r.SharedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<SharedReport>> FindBySharedByIdAsync(int sharedById)
        => await Context.Set<SharedReport>()
            .Where(r => r.SharedById == sharedById)
            .OrderByDescending(r => r.SharedAt)
            .ToListAsync();
}

public class PushDeviceRepository(AppDbContext context) : BaseRepository<PushDevice>(context), IPushDeviceRepository
{
    public async Task<PushDevice?> FindByFarmIdAsync(int farmId)
        => await Context.Set<PushDevice>().FirstOrDefaultAsync(d => d.FarmId == farmId);

    public async Task<PushDevice?> FindByDeviceTokenAsync(string deviceToken)
        => await Context.Set<PushDevice>().FirstOrDefaultAsync(d => d.DeviceToken == deviceToken);
}

public class NotificationSettingRepository(AppDbContext context) : BaseRepository<NotificationSetting>(context), INotificationSettingRepository
{
    public async Task<NotificationSetting?> FindByFarmIdAsync(int farmId)
        => await Context.Set<NotificationSetting>().FirstOrDefaultAsync(n => n.FarmId == farmId);
}
