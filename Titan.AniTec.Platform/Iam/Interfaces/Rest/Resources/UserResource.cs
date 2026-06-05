namespace Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;

public record UserResource(int Id, string Username, string Email, string Role, bool IsActive, bool EmailVerified, DateTimeOffset? CreatedAt);
