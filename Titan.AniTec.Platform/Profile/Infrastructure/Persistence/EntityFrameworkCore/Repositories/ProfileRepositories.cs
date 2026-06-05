using Microsoft.EntityFrameworkCore;
using Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Profile.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Titan.AniTec.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Titan.AniTec.Platform.Profile.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserProfileRepository(AppDbContext context) : BaseRepository<UserProfile>(context), IUserProfileRepository
{
    public async Task<UserProfile?> FindByUserIdAsync(int userId)
        => await Context.Set<UserProfile>().FirstOrDefaultAsync(p => p.UserId == userId);

    public async Task<bool> ExistsByUserIdAsync(int userId)
        => await Context.Set<UserProfile>().AnyAsync(p => p.UserId == userId);
}

public class FarmRepository(AppDbContext context) : BaseRepository<Farm>(context), IFarmRepository
{
    public async Task<Farm?> FindByUserIdAsync(int userId)
        => await Context.Set<Farm>()
            .Include(f => f.Locations)
            .Include(f => f.Staff)
            .Include(f => f.Certifications)
            .FirstOrDefaultAsync(f => f.UserId == userId);

    public async Task<FarmLocation?> FindLocationByIdAsync(int locationId)
        => await Context.Set<FarmLocation>().FirstOrDefaultAsync(l => l.Id == locationId);

    public async Task<FarmStaff?> FindStaffByIdAsync(int staffId)
        => await Context.Set<FarmStaff>().FirstOrDefaultAsync(s => s.Id == staffId);

    public async Task<FarmCertification?> FindCertificationByIdAsync(int certificationId)
        => await Context.Set<FarmCertification>().FirstOrDefaultAsync(c => c.Id == certificationId);
}

public class ClinicRepository(AppDbContext context) : BaseRepository<Clinic>(context), IClinicRepository
{
    public async Task<Clinic?> FindByUserIdAsync(int userId)
        => await Context.Set<Clinic>()
            .Include(c => c.Schedules)
            .Include(c => c.Specialties)
            .Include(c => c.ServiceAreaLocations)
            .Include(c => c.Availabilities)
            .FirstOrDefaultAsync(c => c.UserId == userId);

    public async Task<Specialty?> FindSpecialtyByIdAsync(int specialtyId)
        => await Context.Set<Specialty>().FirstOrDefaultAsync(s => s.Id == specialtyId);

    public async Task<ServiceAreaLocation?> FindServiceAreaLocationByIdAsync(int locationId)
        => await Context.Set<ServiceAreaLocation>().FirstOrDefaultAsync(l => l.Id == locationId);

    public async Task<ProfessionalLicense?> FindLicenseByClinicIdAsync(int clinicId)
        => await Context.Set<ProfessionalLicense>().FirstOrDefaultAsync(l => l.ClinicId == clinicId);
}

public class UserPreferencesRepository(AppDbContext context) : BaseRepository<UserPreferences>(context), IUserPreferencesRepository
{
    public async Task<UserPreferences?> FindByUserIdAsync(int userId)
        => await Context.Set<UserPreferences>().FirstOrDefaultAsync(p => p.UserId == userId);
}

public class NotificationRepository(AppDbContext context) : BaseRepository<Notification>(context), INotificationRepository
{
    public async Task<IReadOnlyCollection<Notification>> FindByUserIdAsync(int userId)
        => await Context.Set<Notification>()
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

    public async Task<IReadOnlyCollection<Notification>> FindUnreadByUserIdAsync(int userId)
        => await Context.Set<Notification>()
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToListAsync();
}
