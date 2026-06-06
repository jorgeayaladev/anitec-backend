using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Admin.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace Titan.AniTec.Platform.Admin.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SystemSettingRepository(AppDbContext context) : ISystemSettingRepository
{
    public async Task<SystemSetting?> FindByKeyAsync(string key) =>
        await context.Set<SystemSetting>().FirstOrDefaultAsync(s => s.Key == key);

    public async Task<IReadOnlyCollection<SystemSetting>> FindByCategoryAsync(string category) =>
        await context.Set<SystemSetting>().Where(s => s.Category == category).ToListAsync();

    public async Task<IReadOnlyCollection<SystemSetting>> GetAllAsync() =>
        await context.Set<SystemSetting>().ToListAsync();

    public async Task AddAsync(SystemSetting setting) =>
        await context.Set<SystemSetting>().AddAsync(setting);
}

public class AuditLogRepository(AppDbContext context) : IAuditLogRepository
{
    public async Task<AuditLog?> FindByIdAsync(int id) =>
        await context.Set<AuditLog>().FindAsync(id);

    public async Task<IReadOnlyCollection<AuditLog>> GetAllAsync() =>
        await context.Set<AuditLog>().OrderByDescending(l => l.CreatedAt).ToListAsync();

    public async Task<IReadOnlyCollection<AuditLog>> FindByUserIdAsync(int userId) =>
        await context.Set<AuditLog>().Where(l => l.UserId == userId).OrderByDescending(l => l.CreatedAt).ToListAsync();

    public async Task<IReadOnlyCollection<AuditLog>> FindByEntityAsync(string entityType, int entityId) =>
        await context.Set<AuditLog>().Where(l => l.EntityType == entityType && l.EntityId == entityId)
            .OrderByDescending(l => l.CreatedAt).ToListAsync();

    public async Task AddAsync(AuditLog log) =>
        await context.Set<AuditLog>().AddAsync(log);
}

public class ContentPageRepository(AppDbContext context) : IContentPageRepository
{
    public async Task<ContentPage?> FindByIdAsync(int id) =>
        await context.Set<ContentPage>().FindAsync(id);

    public async Task<IReadOnlyCollection<ContentPage>> GetAllAsync() =>
        await context.Set<ContentPage>().ToListAsync();

    public async Task AddAsync(ContentPage page) =>
        await context.Set<ContentPage>().AddAsync(page);
}

public class FaqRepository(AppDbContext context) : IFaqRepository
{
    public async Task<Faq?> FindByIdAsync(int id) =>
        await context.Set<Faq>().FindAsync(id);

    public async Task<IReadOnlyCollection<Faq>> GetAllAsync() =>
        await context.Set<Faq>().OrderBy(f => f.SortOrder).ToListAsync();

    public async Task AddAsync(Faq faq) =>
        await context.Set<Faq>().AddAsync(faq);

    public void Remove(Faq faq) =>
        context.Set<Faq>().Remove(faq);
}

public class AnnouncementRepository(AppDbContext context) : IAnnouncementRepository
{
    public async Task<Announcement?> FindByIdAsync(int id) =>
        await context.Set<Announcement>().FindAsync(id);

    public async Task<IReadOnlyCollection<Announcement>> GetAllAsync() =>
        await context.Set<Announcement>().OrderByDescending(a => a.CreatedAt).ToListAsync();

    public async Task AddAsync(Announcement announcement) =>
        await context.Set<Announcement>().AddAsync(announcement);

    public void Remove(Announcement announcement) =>
        context.Set<Announcement>().Remove(announcement);
}
