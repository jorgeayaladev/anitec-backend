namespace Titan.AniTec.Platform.Iam.Domain.Model.Commands;

public record UpdateUserRoleCommand(int UserId, string Role);
