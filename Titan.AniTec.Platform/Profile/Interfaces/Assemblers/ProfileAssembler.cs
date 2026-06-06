using Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Profile.Domain.Repositories;
using Titan.AniTec.Platform.Profile.Interfaces.Resources;
using Titan.AniTec.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Titan.AniTec.Platform.Profile.Interfaces.Assemblers;

public static class ProfileAssembler
{
    public static UpdateProfileCommand ToCommand(int userId, UpdateProfileResource resource)
        => new(userId, resource.FirstName, resource.LastName, resource.Phone, resource.Bio);

    public static ProfileResource ToResource(UserProfile entity)
        => new(entity.Id, entity.UserId, entity.FirstName, entity.LastName,
            entity.Phone, entity.AvatarUrl, entity.Bio);

    public static UpdatePreferencesCommand ToCommand(int userId, UpdatePreferencesResource resource)
        => new(userId, resource.Language, resource.Theme);

    public static PreferencesResource ToResource(UserPreferences entity)
        => new(entity.Language, entity.Theme, entity.NotificationsEnabled,
            entity.EmailNotifications, entity.PushNotifications, entity.SmsNotifications);

    public static UpdateNotificationSettingsCommand ToCommand(int userId, UpdateNotificationSettingsResource resource)
        => new(userId, resource.NotificationsEnabled, resource.EmailNotifications,
            resource.PushNotifications, resource.SmsNotifications);

    public static NotificationResource ToResource(Notification entity)
        => new(entity.Id, entity.Type, entity.Title, entity.Message,
            entity.IsRead, entity.ReadAt, entity.CreatedAt!.Value);

    public static UpdateFarmCommand ToCommand(int userId, UpdateFarmResource resource)
        => new(userId, resource.FarmName, resource.FarmSize, resource.Address, resource.City,
            resource.State, resource.Country, resource.PostalCode, resource.Latitude,
            resource.Longitude, resource.Description);

    public static FarmResource ToResource(Farm entity)
        => new(entity.Id, entity.FarmName, entity.FarmSize, entity.Address, entity.City,
            entity.State, entity.Country, entity.PostalCode, entity.Latitude,
            entity.Longitude, entity.Description);

    public static FarmLocationResource ToResource(FarmLocation entity)
        => new(entity.Id, entity.Name, entity.Description, entity.Area);

    public static AddFarmLocationCommand ToCommand(int userId, CreateFarmLocationResource resource)
        => new(userId, resource.Name, resource.Description, resource.Area);

    public static UpdateFarmLocationCommand ToCommand(int userId, int locationId, UpdateFarmLocationResource resource)
        => new(userId, locationId, resource.Name, resource.Description, resource.Area);

    public static FarmStaffResource ToResource(FarmStaff entity)
        => new(entity.Id, entity.FullName, entity.Role, entity.Phone, entity.Email);

    public static AddFarmStaffCommand ToCommand(int userId, CreateFarmStaffResource resource)
        => new(userId, resource.FullName, resource.Role, resource.Phone, resource.Email);

    public static UpdateFarmStaffCommand ToCommand(int userId, int staffId, UpdateFarmStaffResource resource)
        => new(userId, staffId, resource.FullName, resource.Role, resource.Phone, resource.Email);

    public static FarmCertificationResource ToResource(FarmCertification entity)
        => new(entity.Id, entity.Name, entity.IssuingAuthority, entity.IssueDate,
            entity.ExpiryDate, entity.CertificationNumber);

    public static AddFarmCertificationCommand ToCommand(int userId, CreateFarmCertificationResource resource)
        => new(userId, resource.Name, resource.IssuingAuthority, resource.IssueDate,
            resource.ExpiryDate, resource.CertificationNumber);

    public static UpdateFarmCertificationCommand ToCommand(int userId, int certificationId, UpdateFarmCertificationResource resource)
        => new(userId, certificationId, resource.Name, resource.IssuingAuthority,
            resource.IssueDate, resource.ExpiryDate, resource.CertificationNumber);

    public static UpdateClinicCommand ToCommand(int userId, UpdateClinicResource resource)
        => new(userId, resource.ClinicName, resource.Address, resource.City, resource.State,
            resource.Country, resource.PostalCode, resource.Latitude, resource.Longitude,
            resource.Phone, resource.Email, resource.Description);

    public static ClinicResource ToResource(Clinic entity)
        => new(entity.Id, entity.ClinicName, entity.Address, entity.City, entity.State,
            entity.Country, entity.PostalCode, entity.Latitude, entity.Longitude,
            entity.Phone, entity.Email, entity.Description);

    public static ClinicScheduleResource ToResource(ClinicSchedule entity)
        => new(entity.DayOfWeek, entity.OpenTime, entity.CloseTime, entity.IsAvailable);

    public static UpdateClinicScheduleCommand ToCommand(int userId, UpdateClinicScheduleResource resource)
        => new(userId, resource.Schedules.Select(s => new ClinicScheduleItem(s.DayOfWeek, s.OpenTime, s.CloseTime, s.IsAvailable)).ToList());

    public static SpecialtyResource ToResource(Specialty entity)
        => new(entity.Id, entity.Name, entity.Description);

    public static AddSpecialtyCommand ToCommand(int userId, CreateSpecialtyResource resource)
        => new(userId, resource.Name, resource.Description);

    public static ProfessionalLicenseResource ToResource(ProfessionalLicense entity)
        => new(entity.Id, entity.LicenseNumber, entity.IssuingAuthority,
            entity.IssueDate, entity.ExpiryDate, entity.IsVerified);

    public static UpdateProfessionalLicenseCommand ToCommand(int userId, UpdateProfessionalLicenseResource resource)
        => new(userId, resource.LicenseNumber, resource.IssuingAuthority,
            resource.IssueDate, resource.ExpiryDate, resource.IsVerified);

    public static ServiceAreaLocationResource ToResource(ServiceAreaLocation entity)
        => new(entity.Id, entity.Name, entity.Address, entity.Latitude, entity.Longitude);

    public static AddServiceAreaLocationCommand ToCommand(int userId, CreateServiceAreaLocationResource resource)
        => new(userId, resource.Name, resource.Address, resource.Latitude, resource.Longitude);

    public static AvailabilityItemResource ToResource(ClinicAvailability entity)
        => new(entity.DayOfWeek, entity.StartTime, entity.EndTime, entity.IsAvailable);

    public static UpdateAvailabilityCommand ToCommand(int userId, UpdateAvailabilityResource resource)
        => new(userId, resource.Availabilities.Select(a => new AvailabilityItem(a.DayOfWeek, a.StartTime, a.EndTime, a.IsAvailable)).ToList());

    public static UpdateServiceAreaCommand ToCommand(int userId, UpdateServiceAreaResource resource)
        => new(userId, resource.Locations.Select(l => new ServiceAreaLocationItem(l.Name, l.Address, l.Latitude, l.Longitude)).ToList());
}

public static class ProfileActionResultAssembler
{
    public static IActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Data);

        return result.Error switch
        {
            Domain.Model.ProfileError.ProfileNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.FarmNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.FarmLocationNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.FarmStaffNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.FarmCertificationNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ClinicNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ClinicScheduleNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.SpecialtyNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ProfessionalLicenseNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ServiceAreaLocationNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ClinicAvailabilityNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.PreferencesNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.NotificationNotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToActionResult(Result result)
    {
        if (result.IsSuccess)
            return new OkResult();

        return result.Error switch
        {
            Domain.Model.ProfileError.ProfileNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.FarmNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.FarmLocationNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.FarmStaffNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.FarmCertificationNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ClinicNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ClinicScheduleNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.SpecialtyNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ProfessionalLicenseNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ServiceAreaLocationNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.ClinicAvailabilityNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.PreferencesNotFound => new NotFoundResult(),
            Domain.Model.ProfileError.NotificationNotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Title = "Bad Request",
                Detail = result.Error?.ToString() ?? "Unknown error",
                Status = 400
            })
        };
    }

    public static IActionResult ToCreatedActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return new CreatedResult(string.Empty, result.Value);

        return ToActionResult(result);
    }
}
