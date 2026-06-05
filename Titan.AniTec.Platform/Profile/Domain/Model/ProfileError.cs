namespace Titan.AniTec.Platform.Profile.Domain.Model;

public enum ProfileError
{
    ProfileNotFound,
    ProfileAlreadyExists,
    FarmNotFound,
    FarmLocationNotFound,
    FarmStaffNotFound,
    FarmCertificationNotFound,
    ClinicNotFound,
    ClinicScheduleNotFound,
    SpecialtyNotFound,
    ProfessionalLicenseNotFound,
    ServiceAreaLocationNotFound,
    ClinicAvailabilityNotFound,
    PreferencesNotFound,
    NotificationNotFound,
    InvalidFarmData,
    InvalidClinicData,
    InvalidScheduleData,
    InvalidLicenseData,
    InvalidAvailabilityData,
    AvatarUploadFailed,
    AvatarDeleteFailed,
    UnauthorizedAccess
}
