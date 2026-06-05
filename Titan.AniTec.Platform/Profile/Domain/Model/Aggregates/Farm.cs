using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;

public class Farm : IAuditableEntity
{
    private readonly List<FarmLocation> _locations = [];
    private readonly List<FarmStaff> _staff = [];
    private readonly List<FarmCertification> _certifications = [];

    public Farm(int userId, string farmName)
    {
        UserId = userId;
        FarmName = farmName;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string FarmName { get; private set; }
    public double? FarmSize { get; private set; }
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? Country { get; private set; }
    public string? PostalCode { get; private set; }
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }
    public string? Description { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public IReadOnlyCollection<FarmLocation> Locations => _locations.AsReadOnly();
    public IReadOnlyCollection<FarmStaff> Staff => _staff.AsReadOnly();
    public IReadOnlyCollection<FarmCertification> Certifications => _certifications.AsReadOnly();

    public Farm UpdateDetails(string farmName, double? farmSize, string? address, string? city,
        string? state, string? country, string? postalCode, double? latitude, double? longitude, string? description)
    {
        FarmName = farmName;
        FarmSize = farmSize;
        Address = address;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        return this;
    }

    public void AddLocation(FarmLocation location) => _locations.Add(location);
    public void RemoveLocation(FarmLocation location) => _locations.Remove(location);
    public void AddStaff(FarmStaff staff) => _staff.Add(staff);
    public void RemoveStaff(FarmStaff staff) => _staff.Remove(staff);
    public void AddCertification(FarmCertification certification) => _certifications.Add(certification);
    public void RemoveCertification(FarmCertification certification) => _certifications.Remove(certification);
}

public class FarmLocation : IAuditableEntity
{
    public FarmLocation(string name, string? description, double? area)
    {
        Name = name;
        Description = description;
        Area = area;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public double? Area { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void Update(string name, string? description, double? area)
    {
        Name = name;
        Description = description;
        Area = area;
    }
}

public class FarmStaff : IAuditableEntity
{
    public FarmStaff(string fullName, string role, string? phone, string? email)
    {
        FullName = fullName;
        Role = role;
        Phone = phone;
        Email = email;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string FullName { get; private set; }
    public string Role { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void Update(string fullName, string role, string? phone, string? email)
    {
        FullName = fullName;
        Role = role;
        Phone = phone;
        Email = email;
    }
}

public class FarmCertification : IAuditableEntity
{
    public FarmCertification(string name, string issuingAuthority, DateTimeOffset? issueDate,
        DateTimeOffset? expiryDate, string? certificationNumber)
    {
        Name = name;
        IssuingAuthority = issuingAuthority;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        CertificationNumber = certificationNumber;
    }

    public int Id { get; private set; }
    public int FarmId { get; private set; }
    public string Name { get; private set; }
    public string IssuingAuthority { get; private set; }
    public DateTimeOffset? IssueDate { get; private set; }
    public DateTimeOffset? ExpiryDate { get; private set; }
    public string? CertificationNumber { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void Update(string name, string issuingAuthority, DateTimeOffset? issueDate,
        DateTimeOffset? expiryDate, string? certificationNumber)
    {
        Name = name;
        IssuingAuthority = issuingAuthority;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        CertificationNumber = certificationNumber;
    }
}
