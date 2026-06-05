using System.Text.Json.Serialization;
using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Iam.Domain.Model.Aggregates;

public partial class User : IAuditableEntity
{
    public User(string username, string email, string passwordHash, string role)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;
        EmailVerified = false;
    }

    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Role { get; private set; }

    [JsonIgnore]
    public string PasswordHash { get; private set; }

    public bool IsActive { get; private set; }
    public bool EmailVerified { get; private set; }
    public DateTimeOffset? EmailVerifiedAt { get; private set; }
    public DateTimeOffset? LastLoginAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    public User UpdateEmail(string email)
    {
        Email = email;
        return this;
    }

    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }

    public User UpdateRole(string role)
    {
        Role = role;
        return this;
    }

    public User Activate()
    {
        IsActive = true;
        return this;
    }

    public User Deactivate()
    {
        IsActive = false;
        return this;
    }

    public User VerifyEmail()
    {
        EmailVerified = true;
        EmailVerifiedAt = DateTimeOffset.UtcNow;
        return this;
    }

    public User MarkLogin()
    {
        LastLoginAt = DateTimeOffset.UtcNow;
        return this;
    }
}
