namespace Titan.AniTec.Platform.Profile.Domain.Repositories;

public record UpdateProfileCommand(int UserId, string FirstName, string LastName, string? Phone, string? Bio);

public record UpdatePreferencesCommand(int UserId, string? Language, string? Theme);

public record UpdateNotificationSettingsCommand(int UserId, bool? NotificationsEnabled, bool? EmailNotifications, bool? PushNotifications, bool? SmsNotifications);

public record MarkNotificationReadCommand(int UserId, int NotificationId);

public record MarkAllNotificationsReadCommand(int UserId);

public record UpdateFarmCommand(
    int UserId, string FarmName, double? FarmSize, string? Address, string? City,
    string? State, string? Country, string? PostalCode, double? Latitude, double? Longitude, string? Description);

public record AddFarmLocationCommand(int UserId, string Name, string? Description, double? Area);
public record UpdateFarmLocationCommand(int UserId, int LocationId, string Name, string? Description, double? Area);
public record RemoveFarmLocationCommand(int UserId, int LocationId);

public record AddFarmStaffCommand(int UserId, string FullName, string Role, string? Phone, string? Email);
public record UpdateFarmStaffCommand(int UserId, int StaffId, string FullName, string Role, string? Phone, string? Email);
public record RemoveFarmStaffCommand(int UserId, int StaffId);

public record AddFarmCertificationCommand(int UserId, string Name, string IssuingAuthority,
    DateTimeOffset? IssueDate, DateTimeOffset? ExpiryDate, string? CertificationNumber);
public record UpdateFarmCertificationCommand(int UserId, int CertificationId, string Name, string IssuingAuthority,
    DateTimeOffset? IssueDate, DateTimeOffset? ExpiryDate, string? CertificationNumber);
public record RemoveFarmCertificationCommand(int UserId, int CertificationId);

public record UpdateClinicCommand(
    int UserId, string ClinicName, string? Address, string? City, string? State, string? Country,
    string? PostalCode, double? Latitude, double? Longitude, string? Phone, string? Email, string? Description);

public record UpdateClinicScheduleCommand(int UserId, IReadOnlyCollection<ClinicScheduleItem> Schedules);
public record ClinicScheduleItem(DayOfWeek DayOfWeek, TimeSpan OpenTime, TimeSpan CloseTime, bool IsAvailable);

public record AddSpecialtyCommand(int UserId, string Name, string? Description);
public record RemoveSpecialtyCommand(int UserId, int SpecialtyId);

public record UpdateProfessionalLicenseCommand(int UserId, string LicenseNumber, string IssuingAuthority,
    DateTimeOffset? IssueDate, DateTimeOffset? ExpiryDate, bool IsVerified);

public record AddServiceAreaLocationCommand(int UserId, string Name, string? Address, double? Latitude, double? Longitude);
public record RemoveServiceAreaLocationCommand(int UserId, int LocationId);

public record UpdateAvailabilityCommand(int UserId, IReadOnlyCollection<AvailabilityItem> Availabilities);
public record AvailabilityItem(DayOfWeek DayOfWeek, TimeSpan StartTime, TimeSpan EndTime, bool IsAvailable);

public record UploadAvatarCommand(int UserId, byte[] ImageData, string FileName);
public record DeleteAvatarCommand(int UserId);
