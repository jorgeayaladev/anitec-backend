namespace Titan.AniTec.Platform.Profile.Domain.Repositories;

public record GetMyProfileQuery(int UserId);
public record GetMyPreferencesQuery(int UserId);
public record GetNotificationHistoryQuery(int UserId);
public record GetFarmQuery(int UserId);
public record GetFarmLocationsQuery(int UserId);
public record GetFarmStaffQuery(int UserId);
public record GetFarmCertificationsQuery(int UserId);
public record GetClinicQuery(int UserId);
public record GetClinicScheduleQuery(int UserId);
public record GetSpecialtiesQuery(int UserId);
public record GetProfessionalLicenseQuery(int UserId);
public record GetServiceAreaQuery(int UserId);
public record GetAvailabilityQuery(int UserId);
