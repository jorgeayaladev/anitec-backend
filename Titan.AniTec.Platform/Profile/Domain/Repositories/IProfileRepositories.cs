using Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Profile.Domain.Repositories;

public interface IUserProfileRepository : IBaseRepository<UserProfile>
{
    Task<UserProfile?> FindByUserIdAsync(int userId);
    Task<bool> ExistsByUserIdAsync(int userId);
}

public interface IFarmRepository : IBaseRepository<Farm>
{
    Task<Farm?> FindByUserIdAsync(int userId);
    Task<FarmLocation?> FindLocationByIdAsync(int locationId);
    Task<FarmStaff?> FindStaffByIdAsync(int staffId);
    Task<FarmCertification?> FindCertificationByIdAsync(int certificationId);
}

public interface IClinicRepository : IBaseRepository<Clinic>
{
    Task<Clinic?> FindByUserIdAsync(int userId);
    Task<Specialty?> FindSpecialtyByIdAsync(int specialtyId);
    Task<ServiceAreaLocation?> FindServiceAreaLocationByIdAsync(int locationId);
    Task<ProfessionalLicense?> FindLicenseByClinicIdAsync(int clinicId);
}

public interface IUserPreferencesRepository : IBaseRepository<UserPreferences>
{
    Task<UserPreferences?> FindByUserIdAsync(int userId);
}

public interface INotificationRepository : IBaseRepository<Notification>
{
    Task<IReadOnlyCollection<Notification>> FindByUserIdAsync(int userId);
    Task<IReadOnlyCollection<Notification>> FindUnreadByUserIdAsync(int userId);
}
