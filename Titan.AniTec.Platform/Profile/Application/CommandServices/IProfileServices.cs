using Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Profile.Domain.Repositories;
using Titan.AniTec.Platform.Shared.Application.Model;

namespace Titan.AniTec.Platform.Profile.Application.CommandServices;

public interface IProfileCommandService
{
    Task<Result<UserProfile>> UpdateProfileAsync(UpdateProfileCommand command);
    Task<Result<UserPreferences>> UpdatePreferencesAsync(UpdatePreferencesCommand command);
    Task<Result<UserPreferences>> UpdateNotificationSettingsAsync(UpdateNotificationSettingsCommand command);
    Task<Result> MarkNotificationReadAsync(MarkNotificationReadCommand command);
    Task<Result> MarkAllNotificationsReadAsync(MarkAllNotificationsReadCommand command);
    Task<Result<Farm>> UpdateFarmAsync(UpdateFarmCommand command);
    Task<Result<FarmLocation>> AddFarmLocationAsync(AddFarmLocationCommand command);
    Task<Result<FarmLocation>> UpdateFarmLocationAsync(UpdateFarmLocationCommand command);
    Task<Result> RemoveFarmLocationAsync(RemoveFarmLocationCommand command);
    Task<Result<FarmStaff>> AddFarmStaffAsync(AddFarmStaffCommand command);
    Task<Result<FarmStaff>> UpdateFarmStaffAsync(UpdateFarmStaffCommand command);
    Task<Result> RemoveFarmStaffAsync(RemoveFarmStaffCommand command);
    Task<Result<FarmCertification>> AddFarmCertificationAsync(AddFarmCertificationCommand command);
    Task<Result<FarmCertification>> UpdateFarmCertificationAsync(UpdateFarmCertificationCommand command);
    Task<Result> RemoveFarmCertificationAsync(RemoveFarmCertificationCommand command);
    Task<Result<Clinic>> UpdateClinicAsync(UpdateClinicCommand command);
    Task<Result> UpdateClinicScheduleAsync(UpdateClinicScheduleCommand command);
    Task<Result<Specialty>> AddSpecialtyAsync(AddSpecialtyCommand command);
    Task<Result> RemoveSpecialtyAsync(RemoveSpecialtyCommand command);
    Task<Result<ProfessionalLicense>> UpdateProfessionalLicenseAsync(UpdateProfessionalLicenseCommand command);
    Task<Result<ServiceAreaLocation>> AddServiceAreaLocationAsync(AddServiceAreaLocationCommand command);
    Task<Result> RemoveServiceAreaLocationAsync(RemoveServiceAreaLocationCommand command);
    Task<Result> UpdateAvailabilityAsync(UpdateAvailabilityCommand command);
    Task<Result<UserProfile>> UploadAvatarAsync(UploadAvatarCommand command);
    Task<Result> DeleteAvatarAsync(DeleteAvatarCommand command);
    Task<Result> UpdateServiceAreaAsync(UpdateServiceAreaCommand command);
}

public interface IProfileQueryService
{
    Task<Result<UserProfile>> GetMyProfileAsync(GetMyProfileQuery query);
    Task<Result<UserPreferences>> GetMyPreferencesAsync(GetMyPreferencesQuery query);
    Task<Result<IReadOnlyCollection<Notification>>> GetNotificationHistoryAsync(GetNotificationHistoryQuery query);
    Task<Result<Farm>> GetFarmAsync(GetFarmQuery query);
    Task<Result<IReadOnlyCollection<FarmLocation>>> GetFarmLocationsAsync(GetFarmLocationsQuery query);
    Task<Result<IReadOnlyCollection<FarmStaff>>> GetFarmStaffAsync(GetFarmStaffQuery query);
    Task<Result<IReadOnlyCollection<FarmCertification>>> GetFarmCertificationsAsync(GetFarmCertificationsQuery query);
    Task<Result<Clinic>> GetClinicAsync(GetClinicQuery query);
    Task<Result<IReadOnlyCollection<ClinicSchedule>>> GetClinicScheduleAsync(GetClinicScheduleQuery query);
    Task<Result<IReadOnlyCollection<Specialty>>> GetSpecialtiesAsync(GetSpecialtiesQuery query);
    Task<Result<ProfessionalLicense>> GetProfessionalLicenseAsync(GetProfessionalLicenseQuery query);
    Task<Result<IReadOnlyCollection<ServiceAreaLocation>>> GetServiceAreaAsync(GetServiceAreaQuery query);
    Task<Result<IReadOnlyCollection<ClinicAvailability>>> GetAvailabilityAsync(GetAvailabilityQuery query);
}
