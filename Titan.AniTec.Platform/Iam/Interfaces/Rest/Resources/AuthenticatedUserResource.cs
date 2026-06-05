namespace Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;

public record AuthenticatedUserResource(int Id, string Username, string Email, string Role, string Token);
