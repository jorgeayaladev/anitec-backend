using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;

public class ProfessionalLicense : IAuditableEntity
{
    public ProfessionalLicense(int clinicId, string licenseNumber, string issuingAuthority,
        DateTimeOffset? issueDate, DateTimeOffset? expiryDate, bool isVerified)
    {
        ClinicId = clinicId;
        LicenseNumber = licenseNumber;
        IssuingAuthority = issuingAuthority;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        IsVerified = isVerified;
    }

    public int Id { get; private set; }
    public int ClinicId { get; private set; }
    public string LicenseNumber { get; private set; }
    public string IssuingAuthority { get; private set; }
    public DateTimeOffset? IssueDate { get; private set; }
    public DateTimeOffset? ExpiryDate { get; private set; }
    public bool IsVerified { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void Update(string licenseNumber, string issuingAuthority,
        DateTimeOffset? issueDate, DateTimeOffset? expiryDate, bool isVerified)
    {
        LicenseNumber = licenseNumber;
        IssuingAuthority = issuingAuthority;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        IsVerified = isVerified;
    }
}
