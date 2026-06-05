using Titan.AniTec.Platform.Shared.Domain.Model.Entities;

namespace Titan.AniTec.Platform.Profile.Domain.Model.Aggregates;

public class UserProfile : IAuditableEntity
{
    public UserProfile(int userId, string firstName, string lastName)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? Phone { get; private set; }
    public string? AvatarUrl { get; private set; }
    public string? Bio { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public UserProfile UpdateName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        return this;
    }

    public UserProfile UpdatePhone(string? phone)
    {
        Phone = phone;
        return this;
    }

    public UserProfile UpdateBio(string? bio)
    {
        Bio = bio;
        return this;
    }

    public UserProfile UpdateAvatar(string? avatarUrl)
    {
        AvatarUrl = avatarUrl;
        return this;
    }
}
