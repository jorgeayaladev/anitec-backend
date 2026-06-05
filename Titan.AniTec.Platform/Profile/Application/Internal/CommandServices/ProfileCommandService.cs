using Titan.AniTec.Platform.Profile.Domain.Model;
using Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;
using Titan.AniTec.Platform.Profile.Domain.Repositories;
using Titan.AniTec.Platform.Profile.Application.CommandServices;
using Titan.AniTec.Platform.Shared.Application.Model;
using Titan.AniTec.Platform.Shared.Domain.Repositories;

namespace Titan.AniTec.Platform.Profile.Application.Internal.CommandServices;

public class ProfileCommandService(
    IUserProfileRepository userProfileRepository,
    IFarmRepository farmRepository,
    IClinicRepository clinicRepository,
    IUserPreferencesRepository userPreferencesRepository,
    INotificationRepository notificationRepository,
    IUnitOfWork unitOfWork) : IProfileCommandService
{
    public async Task<Result<UserProfile>> UpdateProfileAsync(UpdateProfileCommand command)
    {
        try
        {
            var profile = await userProfileRepository.FindByUserIdAsync(command.UserId);
            if (profile == null)
            {
                profile = new UserProfile(command.UserId, command.FirstName, command.LastName);
                await userProfileRepository.AddAsync(profile);
            }
            else
            {
                profile.UpdateName(command.FirstName, command.LastName)
                    .UpdatePhone(command.Phone)
                    .UpdateBio(command.Bio);
            }

            await unitOfWork.CompleteAsync();
            return Result<UserProfile>.Success(profile);
        }
        catch (Exception)
        {
            return Result<UserProfile>.Failure(ProfileError.ProfileNotFound);
        }
    }

    public async Task<Result<UserPreferences>> UpdatePreferencesAsync(UpdatePreferencesCommand command)
    {
        try
        {
            var prefs = await userPreferencesRepository.FindByUserIdAsync(command.UserId);
            if (prefs == null)
            {
                prefs = new UserPreferences(command.UserId);
                await userPreferencesRepository.AddAsync(prefs);
            }

            prefs.Update(command.Language, command.Theme);
            await unitOfWork.CompleteAsync();
            return Result<UserPreferences>.Success(prefs);
        }
        catch (Exception)
        {
            return Result<UserPreferences>.Failure(ProfileError.PreferencesNotFound);
        }
    }

    public async Task<Result<UserPreferences>> UpdateNotificationSettingsAsync(UpdateNotificationSettingsCommand command)
    {
        try
        {
            var prefs = await userPreferencesRepository.FindByUserIdAsync(command.UserId);
            if (prefs == null)
            {
                prefs = new UserPreferences(command.UserId);
                await userPreferencesRepository.AddAsync(prefs);
            }

            prefs.UpdateNotifications(command.NotificationsEnabled, command.EmailNotifications,
                command.PushNotifications, command.SmsNotifications);
            await unitOfWork.CompleteAsync();
            return Result<UserPreferences>.Success(prefs);
        }
        catch (Exception)
        {
            return Result<UserPreferences>.Failure(ProfileError.PreferencesNotFound);
        }
    }

    public async Task<Result> MarkNotificationReadAsync(MarkNotificationReadCommand command)
    {
        try
        {
            var notifications = await notificationRepository.FindByUserIdAsync(command.UserId);
            var notification = notifications.FirstOrDefault(n => n.Id == command.NotificationId);
            if (notification == null)
                return Result.Failure(ProfileError.NotificationNotFound);

            notification.MarkAsRead();
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ProfileError.NotificationNotFound);
        }
    }

    public async Task<Result> MarkAllNotificationsReadAsync(MarkAllNotificationsReadCommand command)
    {
        try
        {
            var notifications = await notificationRepository.FindUnreadByUserIdAsync(command.UserId);
            foreach (var notification in notifications)
                notification.MarkAsRead();

            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ProfileError.NotificationNotFound);
        }
    }

    public async Task<Result<Farm>> UpdateFarmAsync(UpdateFarmCommand command)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(command.UserId);
            if (farm == null)
            {
                farm = new Farm(command.UserId, command.FarmName);
                await farmRepository.AddAsync(farm);
            }

            farm.UpdateDetails(command.FarmName, command.FarmSize, command.Address, command.City,
                command.State, command.Country, command.PostalCode, command.Latitude,
                command.Longitude, command.Description);
            await unitOfWork.CompleteAsync();
            return Result<Farm>.Success(farm);
        }
        catch (Exception)
        {
            return Result<Farm>.Failure(ProfileError.InvalidFarmData);
        }
    }

    public async Task<Result<FarmLocation>> AddFarmLocationAsync(AddFarmLocationCommand command)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(command.UserId);
            if (farm == null)
                return Result<FarmLocation>.Failure(ProfileError.FarmNotFound);

            var location = new FarmLocation(command.Name, command.Description, command.Area);
            farm.AddLocation(location);
            await unitOfWork.CompleteAsync();
            return Result<FarmLocation>.Success(location);
        }
        catch (Exception)
        {
            return Result<FarmLocation>.Failure(ProfileError.InvalidFarmData);
        }
    }

    public async Task<Result<FarmLocation>> UpdateFarmLocationAsync(UpdateFarmLocationCommand command)
    {
        try
        {
            var location = await farmRepository.FindLocationByIdAsync(command.LocationId);
            if (location == null)
                return Result<FarmLocation>.Failure(ProfileError.FarmLocationNotFound);

            location.Update(command.Name, command.Description, command.Area);
            await unitOfWork.CompleteAsync();
            return Result<FarmLocation>.Success(location);
        }
        catch (Exception)
        {
            return Result<FarmLocation>.Failure(ProfileError.FarmLocationNotFound);
        }
    }

    public async Task<Result> RemoveFarmLocationAsync(RemoveFarmLocationCommand command)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(command.UserId);
            if (farm == null)
                return Result.Failure(ProfileError.FarmNotFound);

            var location = farm.Locations.FirstOrDefault(l => l.Id == command.LocationId);
            if (location == null)
                return Result.Failure(ProfileError.FarmLocationNotFound);

            farm.RemoveLocation(location);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ProfileError.FarmLocationNotFound);
        }
    }

    public async Task<Result<FarmStaff>> AddFarmStaffAsync(AddFarmStaffCommand command)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(command.UserId);
            if (farm == null)
                return Result<FarmStaff>.Failure(ProfileError.FarmNotFound);

            var staff = new FarmStaff(command.FullName, command.Role, command.Phone, command.Email);
            farm.AddStaff(staff);
            await unitOfWork.CompleteAsync();
            return Result<FarmStaff>.Success(staff);
        }
        catch (Exception)
        {
            return Result<FarmStaff>.Failure(ProfileError.InvalidFarmData);
        }
    }

    public async Task<Result<FarmStaff>> UpdateFarmStaffAsync(UpdateFarmStaffCommand command)
    {
        try
        {
            var staff = await farmRepository.FindStaffByIdAsync(command.StaffId);
            if (staff == null)
                return Result<FarmStaff>.Failure(ProfileError.FarmStaffNotFound);

            staff.Update(command.FullName, command.Role, command.Phone, command.Email);
            await unitOfWork.CompleteAsync();
            return Result<FarmStaff>.Success(staff);
        }
        catch (Exception)
        {
            return Result<FarmStaff>.Failure(ProfileError.FarmStaffNotFound);
        }
    }

    public async Task<Result> RemoveFarmStaffAsync(RemoveFarmStaffCommand command)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(command.UserId);
            if (farm == null)
                return Result.Failure(ProfileError.FarmNotFound);

            var staff = farm.Staff.FirstOrDefault(s => s.Id == command.StaffId);
            if (staff == null)
                return Result.Failure(ProfileError.FarmStaffNotFound);

            farm.RemoveStaff(staff);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ProfileError.FarmStaffNotFound);
        }
    }

    public async Task<Result<FarmCertification>> AddFarmCertificationAsync(AddFarmCertificationCommand command)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(command.UserId);
            if (farm == null)
                return Result<FarmCertification>.Failure(ProfileError.FarmNotFound);

            var certification = new FarmCertification(command.Name, command.IssuingAuthority,
                command.IssueDate, command.ExpiryDate, command.CertificationNumber);
            farm.AddCertification(certification);
            await unitOfWork.CompleteAsync();
            return Result<FarmCertification>.Success(certification);
        }
        catch (Exception)
        {
            return Result<FarmCertification>.Failure(ProfileError.InvalidFarmData);
        }
    }

    public async Task<Result<FarmCertification>> UpdateFarmCertificationAsync(UpdateFarmCertificationCommand command)
    {
        try
        {
            var certification = await farmRepository.FindCertificationByIdAsync(command.CertificationId);
            if (certification == null)
                return Result<FarmCertification>.Failure(ProfileError.FarmCertificationNotFound);

            certification.Update(command.Name, command.IssuingAuthority,
                command.IssueDate, command.ExpiryDate, command.CertificationNumber);
            await unitOfWork.CompleteAsync();
            return Result<FarmCertification>.Success(certification);
        }
        catch (Exception)
        {
            return Result<FarmCertification>.Failure(ProfileError.FarmCertificationNotFound);
        }
    }

    public async Task<Result> RemoveFarmCertificationAsync(RemoveFarmCertificationCommand command)
    {
        try
        {
            var farm = await farmRepository.FindByUserIdAsync(command.UserId);
            if (farm == null)
                return Result.Failure(ProfileError.FarmNotFound);

            var cert = farm.Certifications.FirstOrDefault(c => c.Id == command.CertificationId);
            if (cert == null)
                return Result.Failure(ProfileError.FarmCertificationNotFound);

            farm.RemoveCertification(cert);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ProfileError.FarmCertificationNotFound);
        }
    }

    public async Task<Result<Clinic>> UpdateClinicAsync(UpdateClinicCommand command)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(command.UserId);
            if (clinic == null)
            {
                clinic = new Clinic(command.UserId, command.ClinicName);
                await clinicRepository.AddAsync(clinic);
            }

            clinic.UpdateDetails(command.ClinicName, command.Address, command.City,
                command.State, command.Country, command.PostalCode, command.Latitude,
                command.Longitude, command.Phone, command.Email, command.Description);
            await unitOfWork.CompleteAsync();
            return Result<Clinic>.Success(clinic);
        }
        catch (Exception)
        {
            return Result<Clinic>.Failure(ProfileError.InvalidClinicData);
        }
    }

    public async Task<Result> UpdateClinicScheduleAsync(UpdateClinicScheduleCommand command)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(command.UserId);
            if (clinic == null)
                return Result.Failure(ProfileError.ClinicNotFound);

            clinic.ClearSchedules();
            foreach (var item in command.Schedules)
                clinic.AddSchedule(new ClinicSchedule(item.DayOfWeek, item.OpenTime, item.CloseTime, item.IsAvailable));

            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ProfileError.InvalidScheduleData);
        }
    }

    public async Task<Result<Specialty>> AddSpecialtyAsync(AddSpecialtyCommand command)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(command.UserId);
            if (clinic == null)
                return Result<Specialty>.Failure(ProfileError.ClinicNotFound);

            var specialty = new Specialty(command.Name, command.Description);
            clinic.AddSpecialty(specialty);
            await unitOfWork.CompleteAsync();
            return Result<Specialty>.Success(specialty);
        }
        catch (Exception)
        {
            return Result<Specialty>.Failure(ProfileError.InvalidClinicData);
        }
    }

    public async Task<Result> RemoveSpecialtyAsync(RemoveSpecialtyCommand command)
    {
        try
        {
            var specialty = await clinicRepository.FindSpecialtyByIdAsync(command.SpecialtyId);
            if (specialty == null)
                return Result.Failure(ProfileError.SpecialtyNotFound);

            var clinic = await clinicRepository.FindByUserIdAsync(command.UserId);
            clinic?.RemoveSpecialty(specialty);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ProfileError.SpecialtyNotFound);
        }
    }

    public async Task<Result<ProfessionalLicense>> UpdateProfessionalLicenseAsync(UpdateProfessionalLicenseCommand command)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(command.UserId);
            if (clinic == null)
                return Result<ProfessionalLicense>.Failure(ProfileError.ClinicNotFound);

            var license = await clinicRepository.FindLicenseByClinicIdAsync(clinic.Id);
            if (license == null)
            {
                license = new ProfessionalLicense(clinic.Id, command.LicenseNumber, command.IssuingAuthority,
                    command.IssueDate, command.ExpiryDate, command.IsVerified);
                await unitOfWork.CompleteAsync();
            }
            else
            {
                license.Update(command.LicenseNumber, command.IssuingAuthority,
                    command.IssueDate, command.ExpiryDate, command.IsVerified);
                await unitOfWork.CompleteAsync();
            }

            return Result<ProfessionalLicense>.Success(license);
        }
        catch (Exception)
        {
            return Result<ProfessionalLicense>.Failure(ProfileError.InvalidLicenseData);
        }
    }

    public async Task<Result<ServiceAreaLocation>> AddServiceAreaLocationAsync(AddServiceAreaLocationCommand command)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(command.UserId);
            if (clinic == null)
                return Result<ServiceAreaLocation>.Failure(ProfileError.ClinicNotFound);

            var location = new ServiceAreaLocation(command.Name, command.Address, command.Latitude, command.Longitude);
            clinic.AddServiceAreaLocation(location);
            await unitOfWork.CompleteAsync();
            return Result<ServiceAreaLocation>.Success(location);
        }
        catch (Exception)
        {
            return Result<ServiceAreaLocation>.Failure(ProfileError.InvalidClinicData);
        }
    }

    public async Task<Result> RemoveServiceAreaLocationAsync(RemoveServiceAreaLocationCommand command)
    {
        try
        {
            var location = await clinicRepository.FindServiceAreaLocationByIdAsync(command.LocationId);
            if (location == null)
                return Result.Failure(ProfileError.ServiceAreaLocationNotFound);

            var clinic = await clinicRepository.FindByUserIdAsync(command.UserId);
            clinic?.RemoveServiceAreaLocation(location);
            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ProfileError.ServiceAreaLocationNotFound);
        }
    }

    public async Task<Result> UpdateAvailabilityAsync(UpdateAvailabilityCommand command)
    {
        try
        {
            var clinic = await clinicRepository.FindByUserIdAsync(command.UserId);
            if (clinic == null)
                return Result.Failure(ProfileError.ClinicNotFound);

            clinic.ClearAvailabilities();
            foreach (var item in command.Availabilities)
                clinic.AddAvailability(new ClinicAvailability(item.DayOfWeek, item.StartTime, item.EndTime, item.IsAvailable));

            await unitOfWork.CompleteAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(ProfileError.InvalidAvailabilityData);
        }
    }
}
