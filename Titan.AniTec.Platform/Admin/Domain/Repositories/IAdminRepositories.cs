using Titan.AniTec.Platform.Admin.Domain.Model.Aggregates;

namespace Titan.AniTec.Platform.Admin.Domain.Repositories;

public interface ISystemSettingRepository
{
    Task<SystemSetting?> FindByKeyAsync(string key);
    Task<IReadOnlyCollection<SystemSetting>> FindByCategoryAsync(string category);
    Task<IReadOnlyCollection<SystemSetting>> GetAllAsync();
    Task AddAsync(SystemSetting setting);
}

public interface IAuditLogRepository
{
    Task<AuditLog?> FindByIdAsync(int id);
    Task<IReadOnlyCollection<AuditLog>> GetAllAsync();
    Task<IReadOnlyCollection<AuditLog>> FindByUserIdAsync(int userId);
    Task<IReadOnlyCollection<AuditLog>> FindByEntityAsync(string entityType, int entityId);
    Task AddAsync(AuditLog log);
}

public interface IContentPageRepository
{
    Task<ContentPage?> FindByIdAsync(int id);
    Task<IReadOnlyCollection<ContentPage>> GetAllAsync();
    Task AddAsync(ContentPage page);
}

public interface IFaqRepository
{
    Task<Faq?> FindByIdAsync(int id);
    Task<IReadOnlyCollection<Faq>> GetAllAsync();
    Task AddAsync(Faq faq);
    void Remove(Faq faq);
}

public interface IAnnouncementRepository
{
    Task<Announcement?> FindByIdAsync(int id);
    Task<IReadOnlyCollection<Announcement>> GetAllAsync();
    Task AddAsync(Announcement announcement);
    void Remove(Announcement announcement);
}
