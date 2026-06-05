using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;

public class Clinic : IAuditableEntity
{
    private readonly List<ClinicSchedule> _schedules = [];
    private readonly List<Specialty> _specialties = [];
    private readonly List<ServiceAreaLocation> _serviceAreaLocations = [];
    private readonly List<ClinicAvailability> _availabilities = [];

    public Clinic(int userId, string clinicName)
    {
        UserId = userId;
        ClinicName = clinicName;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string ClinicName { get; private set; }
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? Country { get; private set; }
    public string? PostalCode { get; private set; }
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public IReadOnlyCollection<ClinicSchedule> Schedules => _schedules.AsReadOnly();
    public IReadOnlyCollection<Specialty> Specialties => _specialties.AsReadOnly();
    public IReadOnlyCollection<ServiceAreaLocation> ServiceAreaLocations => _serviceAreaLocations.AsReadOnly();
    public IReadOnlyCollection<ClinicAvailability> Availabilities => _availabilities.AsReadOnly();

    public Clinic UpdateDetails(string clinicName, string? address, string? city,
        string? state, string? country, string? postalCode, double? latitude, double? longitude,
        string? phone, string? email, string? description)
    {
        ClinicName = clinicName;
        Address = address;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
        Latitude = latitude;
        Longitude = longitude;
        Phone = phone;
        Email = email;
        Description = description;
        return this;
    }

    public void AddSchedule(ClinicSchedule schedule) => _schedules.Add(schedule);
    public void RemoveSchedule(ClinicSchedule schedule) => _schedules.Remove(schedule);
    public void ClearSchedules() => _schedules.Clear();
    public void AddSpecialty(Specialty specialty) => _specialties.Add(specialty);
    public void RemoveSpecialty(Specialty specialty) => _specialties.Remove(specialty);
    public void AddServiceAreaLocation(ServiceAreaLocation location) => _serviceAreaLocations.Add(location);
    public void RemoveServiceAreaLocation(ServiceAreaLocation location) => _serviceAreaLocations.Remove(location);
    public void ClearServiceAreaLocations() => _serviceAreaLocations.Clear();
    public void AddAvailability(ClinicAvailability availability) => _availabilities.Add(availability);
    public void RemoveAvailability(ClinicAvailability availability) => _availabilities.Remove(availability);
    public void ClearAvailabilities() => _availabilities.Clear();
}

public class ClinicSchedule
{
    public ClinicSchedule(DayOfWeek dayOfWeek, TimeSpan openTime, TimeSpan closeTime, bool isAvailable)
    {
        DayOfWeek = dayOfWeek;
        OpenTime = openTime;
        CloseTime = closeTime;
        IsAvailable = isAvailable;
    }

    public int Id { get; private set; }
    public int ClinicId { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeSpan OpenTime { get; private set; }
    public TimeSpan CloseTime { get; private set; }
    public bool IsAvailable { get; private set; }

    public void Update(DayOfWeek dayOfWeek, TimeSpan openTime, TimeSpan closeTime, bool isAvailable)
    {
        DayOfWeek = dayOfWeek;
        OpenTime = openTime;
        CloseTime = closeTime;
        IsAvailable = isAvailable;
    }
}

public class Specialty
{
    public Specialty(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public int Id { get; private set; }
    public int ClinicId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }
}

public class ServiceAreaLocation
{
    public ServiceAreaLocation(string name, string? address, double? latitude, double? longitude)
    {
        Name = name;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public int Id { get; private set; }
    public int ClinicId { get; private set; }
    public string Name { get; private set; }
    public string? Address { get; private set; }
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }

    public void Update(string name, string? address, double? latitude, double? longitude)
    {
        Name = name;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }
}

public class ClinicAvailability
{
    public ClinicAvailability(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime, bool isAvailable)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        IsAvailable = isAvailable;
    }

    public int Id { get; private set; }
    public int ClinicId { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public bool IsAvailable { get; private set; }

    public void Update(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime, bool isAvailable)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        IsAvailable = isAvailable;
    }
}
