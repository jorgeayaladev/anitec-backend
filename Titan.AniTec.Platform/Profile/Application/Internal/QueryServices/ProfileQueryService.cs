using Titan.AniTec.Platform.Profile.Domain.Model;
using Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Profile.Domain.Repositories;
using Titan.AniTec.Platform.Profile.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Profile.Application.Internal.QueryServices;

public class ProfileQueryService(
    IUserProfileRepository userProfileRepository,
    IFarmRepository farmRepository,
    IClinicRepository clinicRepository,
    IUserPreferencesRepository userPreferencesRepository,
    INotificationRepository notificationRepository) : IProfileQueryService
{
    public async Task<Result<UserProfile>> GetMyProfileAsync(GetMyProfileQuery query)
    {
        try
        {
            var profile = await userProfileRepository.FindByUserIdAsync(query.UserId);
            return profile != null
                ? Result<UserProfile>.Success(profile)
                : Result<UserProfile>.Failure(ProfileError.ProfileNotFound);
        }
        catch (Exception)
        {
            return Result<UserProfile>.Failure(ProfileError.ProfileNotFound);
        }
    }

    public async Task<Result<UserPreferences>> GetMyPreferencesAsync(GetMyPreferencesQuery query)
    {
        try
        {
            var prefs = await userPreferencesRepository.FindByUserIdAsync(query.UserId);
            return prefs != null
                ? Result<UserPreferences>.Success(prefs)
                : Result<UserPreferences>.Failure(ProfileError.PreferencesNotFound);
        }
        catch (Exception)
        {
            return Result<UserPreferences>.Failure(ProfileError.PreferencesNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Notification>>> GetNotificationHistoryAsync(GetNotificationHistoryQuery query)
    {
        try
        {
            var notifications = await notificationRepository.FindByUserIdAsync(query.UserId);
            return Result<IReadOnlyCollection<Notification>>.Success(notifications);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Notification>>.Failure(ProfileError.NotificationNotFound);
        }
    }

    public async Task<Result<Farm>> GetFarmAsync(GetFarmQuery query)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(query.UserId);
            return farm != null
                ? Result<Farm>.Success(farm)
                : Result<Farm>.Failure(ProfileError.FarmNotFound);
        }
        catch (Exception)
        {
            return Result<Farm>.Failure(ProfileError.FarmNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmLocation>>> GetFarmLocationsAsync(GetFarmLocationsQuery query)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(query.UserId);
            return farm != null
                ? Result<IReadOnlyCollection<FarmLocation>>.Success(farm.Locations.ToList())
                : Result<IReadOnlyCollection<FarmLocation>>.Failure(ProfileError.FarmNotFound);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmLocation>>.Failure(ProfileError.FarmLocationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmStaff>>> GetFarmStaffAsync(GetFarmStaffQuery query)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(query.UserId);
            return farm != null
                ? Result<IReadOnlyCollection<FarmStaff>>.Success(farm.Staff.ToList())
                : Result<IReadOnlyCollection<FarmStaff>>.Failure(ProfileError.FarmNotFound);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmStaff>>.Failure(ProfileError.FarmStaffNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<FarmCertification>>> GetFarmCertificationsAsync(GetFarmCertificationsQuery query)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(query.UserId);
            return farm != null
                ? Result<IReadOnlyCollection<FarmCertification>>.Success(farm.Certifications.ToList())
                : Result<IReadOnlyCollection<FarmCertification>>.Failure(ProfileError.FarmNotFound);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<FarmCertification>>.Failure(ProfileError.FarmCertificationNotFound);
        }
    }

    public async Task<Result<Clinic>> GetClinicAsync(GetClinicQuery query)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(query.UserId);
            return clinic != null
                ? Result<Clinic>.Success(clinic)
                : Result<Clinic>.Failure(ProfileError.ClinicNotFound);
        }
        catch (Exception)
        {
            return Result<Clinic>.Failure(ProfileError.ClinicNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<ClinicSchedule>>> GetClinicScheduleAsync(GetClinicScheduleQuery query)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(query.UserId);
            return clinic != null
                ? Result<IReadOnlyCollection<ClinicSchedule>>.Success(clinic.Schedules.ToList())
                : Result<IReadOnlyCollection<ClinicSchedule>>.Failure(ProfileError.ClinicNotFound);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<ClinicSchedule>>.Failure(ProfileError.ClinicScheduleNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<Specialty>>> GetSpecialtiesAsync(GetSpecialtiesQuery query)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(query.UserId);
            return clinic != null
                ? Result<IReadOnlyCollection<Specialty>>.Success(clinic.Specialties.ToList())
                : Result<IReadOnlyCollection<Specialty>>.Failure(ProfileError.ClinicNotFound);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<Specialty>>.Failure(ProfileError.SpecialtyNotFound);
        }
    }

    public async Task<Result<ProfessionalLicense>> GetProfessionalLicenseAsync(GetProfessionalLicenseQuery query)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(query.UserId);
            if (clinic == null)
                return Result<ProfessionalLicense>.Failure(ProfileError.ClinicNotFound);

            var license = await clinicRepository.FindLicenseByClinicIdAsync(clinic.Id);
            return license != null
                ? Result<ProfessionalLicense>.Success(license)
                : Result<ProfessionalLicense>.Failure(ProfileError.ProfessionalLicenseNotFound);
        }
        catch (Exception)
        {
            return Result<ProfessionalLicense>.Failure(ProfileError.ProfessionalLicenseNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<ServiceAreaLocation>>> GetServiceAreaAsync(GetServiceAreaQuery query)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(query.UserId);
            return clinic != null
                ? Result<IReadOnlyCollection<ServiceAreaLocation>>.Success(clinic.ServiceAreaLocations.ToList())
                : Result<IReadOnlyCollection<ServiceAreaLocation>>.Failure(ProfileError.ClinicNotFound);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<ServiceAreaLocation>>.Failure(ProfileError.ServiceAreaLocationNotFound);
        }
    }

    public async Task<Result<IReadOnlyCollection<ClinicAvailability>>> GetAvailabilityAsync(GetAvailabilityQuery query)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(query.UserId);
            return clinic != null
                ? Result<IReadOnlyCollection<ClinicAvailability>>.Success(clinic.Availabilities.ToList())
                : Result<IReadOnlyCollection<ClinicAvailability>>.Failure(ProfileError.ClinicNotFound);
        }
        catch (Exception)
        {
            return Result<IReadOnlyCollection<ClinicAvailability>>.Failure(ProfileError.ClinicAvailabilityNotFound);
        }
    }
}
