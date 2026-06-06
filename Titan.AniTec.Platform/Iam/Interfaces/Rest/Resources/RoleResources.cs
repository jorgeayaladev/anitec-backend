namespace Titan.AniTec.Platform.Iam.Interfaces.Rest.Resources;

public record RoleResource(int Id, string Name, string? Description);

public record CreateRoleResource(string Name, string? Description);

public record UpdateRoleResource(string Name, string? Description);
